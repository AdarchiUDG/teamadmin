using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Prometheus.Data.Entities;
using Prometheus.Models.Requests;
using Prometheus.Models.Responses;

namespace Prometheus.Endpoints; 

public class CrudHandler<TEntity, TKey, TDestination> where TEntity : BaseEntity<TKey> {
  private readonly string _pathId;
  private readonly IEndpointRouteBuilder _root;
  
  public CrudHandler(IEndpointRouteBuilder root) {
    _pathId = default(TKey) switch {
      Guid _ => "{id:guid}",
      int => "{id:int}",
      _ => "{id}"
    };
    _root = root;
  }

  public RouteHandlerBuilder MapPaged(Delegate handler) =>
    _root.MapGet("", handler)
      .Produces<PagedResponse<TDestination>>()
      .ProducesValidationProblem();

  public RouteHandlerBuilder MapGetById(Delegate handler) =>
    _root.MapGet(_pathId, handler)
      .Produces<TDestination>()
      .ProducesProblem(StatusCodes.Status404NotFound);

  public RouteHandlerBuilder MapCreate(Delegate handler) =>
    _root.MapPost("", handler)
      .Produces<TDestination>(StatusCodes.Status201Created)
      .ProducesValidationProblem();

  public RouteHandlerBuilder MapUpdate(Delegate handler) =>
    _root.MapPost(_pathId, handler)
      .Produces<TDestination>()
      .ProducesValidationProblem()
      .ProducesProblem(StatusCodes.Status404NotFound);

  public RouteHandlerBuilder MapDelete(Delegate handler) =>
    _root.MapDelete(_pathId, handler)
      .ProducesProblem(StatusCodes.Status404NotFound)
      .Produces(StatusCodes.Status204NoContent);

  public async Task<IStatusCodeHttpResult> Paginate(ListRequest request, IQueryable<TEntity> query) {
    var results = await query
      .Skip(request.Offset)
      .Take(request.Limit)
      .ProjectTo<TDestination>(request.MapperConfig)
      .ToListAsync();

    var total = await query.CountAsync();
    var pages = (int)Math.Ceiling((double)total / request.Limit);
    
    return TypedResults.Ok(new PagedResponse<TDestination>() {
      Results = results,
      Page = request.Page > pages ? pages : request.Page,
      PageSize = results.Count,
      Total = total,
      TotalPages = pages
    });
  }

  public async Task<IStatusCodeHttpResult> GetById(KeyRequest<TKey> request, IQueryable<TEntity> query) {
    var item = await query.Where(u => u.Id!.Equals(request.Id))
      .ProjectTo<TDestination>(request.MapperConfig)
      .FirstOrDefaultAsync();

    return item is null ? TypedResults.Problem("Not found", statusCode: StatusCodes.Status404NotFound)
      : TypedResults.Ok(item);
  }

  public async Task<IStatusCodeHttpResult> Create(Request request, TEntity entity) {
    try {
      await request.Database.AddAsync(entity);
      await request.Database.SaveChangesAsync();
      return TypedResults.Created($"{entity.Id}", request.Mapper.Map<TDestination>(entity));
    } catch (Exception e) {
      return TypedResults.Problem(e.Message, statusCode: StatusCodes.Status400BadRequest);
    }
  }

  public async Task<IStatusCodeHttpResult> Update<TBody>(UpdateRequest<TKey, TBody> request, IQueryable<TEntity> query, Action<TEntity> mapper) {
    var item = await query.Where(u => u.Id!.Equals(request.Id))
      .FirstOrDefaultAsync();

    if (item is null) {
      return TypedResults.Problem("Not found", statusCode: StatusCodes.Status404NotFound);
    }

    try {
      mapper.Invoke(item);
          
      await request.Database.SaveChangesAsync();

      return TypedResults.NoContent();
    } catch (Exception e) {
      return TypedResults.Problem(e.Message, statusCode: StatusCodes.Status400BadRequest);
    }
  }

  public async Task<IStatusCodeHttpResult> Delete(KeyRequest<TKey> request, IQueryable<TEntity> query) {
    var item = await query
      .Where(u => u.Id!.Equals(request.Id))
      .FirstOrDefaultAsync();

    if (item is null) {
      return TypedResults.Problem("Not found", statusCode: StatusCodes.Status404NotFound);
    }

    try {
      item.Deleted = true;
      await request.Database.SaveChangesAsync();
      return TypedResults.NoContent();
    } catch (Exception e) {
      return TypedResults.Problem(e.Message, statusCode: StatusCodes.Status400BadRequest);
    }
  }
}
