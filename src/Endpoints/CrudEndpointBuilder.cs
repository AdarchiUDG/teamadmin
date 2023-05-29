using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Prometheus.Data.Entities;
using Prometheus.Models.Requests;
using Prometheus.Models.Responses;

namespace Prometheus.Endpoints;

public class CrudEndpointBuilder<TSource, TKey, TDestination> 
  where TSource: BaseEntity<TKey> {
  public delegate IQueryable<TSource> QueryGeneratorDelegate<in TRequest>(TRequest request) where TRequest: Request;

  public delegate TSource ObjectMapperDelegate<in TRequest>(TRequest request, out Action<TSource>? onSave)
    where TRequest: Request;
  
  public delegate void ObjectUpdateDelegate<in TRequest>(TRequest request, TSource item, out Action<TSource>? onSave)
    where TRequest: Request;

  private readonly IEndpointRouteBuilder _root;
  private readonly string _pathId;
  public CrudEndpointBuilder(IEndpointRouteBuilder root) {
    _pathId = default(TKey) switch {
      Guid _ => "{id:guid}",
      int => "{id:int}",
      _ => "{id}"
    };
    _root = root;
  }
  public RouteHandlerBuilder MapPaged<TRequest>(QueryGeneratorDelegate<TRequest> queryGenerator)
    where TRequest: ListRequest {
    return _root.MapGet("", async ([AsParameters] TRequest request) => {
        var query = queryGenerator.Invoke(request);
        var results = await query
          .Skip(request.Offset)
          .Take(request.Limit)
          .AsNoTracking()
          .ProjectTo<TDestination>(request.MapperConfig)
          .ToListAsync();

        var total = await query.CountAsync();
        var pages = (int)Math.Ceiling((double)total / request.Limit);

        return Results.Ok(new PagedResponse<TDestination>() {
          Results = results,
          Page = request.Page > pages ? pages : request.Page,
          PageSize = results.Count,
          Total = total,
          TotalPages = pages
        });
      }).Produces<PagedResponse<TDestination>>()
      .ProducesValidationProblem();
  }

  public RouteHandlerBuilder MapGetById<TRequest>(QueryGeneratorDelegate<TRequest> queryGenerator)
    where TRequest: KeyRequest<TKey> {
    return _root.MapGet(_pathId, async ([AsParameters] TRequest request) => {
        var item = await queryGenerator.Invoke(request).Where(u => u.Id.Equals(request.Id))
          .AsNoTracking()
          .ProjectTo<TDestination>(request.MapperConfig)
          .FirstOrDefaultAsync();

        return item is null ? Results.Problem("Not found", statusCode: StatusCodes.Status404NotFound)
          : Results.Ok(item);
      }).Produces<TDestination>()
      .ProducesProblem(StatusCodes.Status404NotFound);
  }

  public RouteHandlerBuilder MapCreate<TRequest>(ObjectMapperDelegate<TRequest> mapper)
    where TRequest: Request {
    return _root.MapPost("", async ([AsParameters] TRequest request) => {
        try {
          var item = mapper.Invoke(request, out var onSave);

          if (!item.Deleted) {
            await request.Database.AddAsync(item);
          } else {
            item.Deleted = false;
          }

          await request.Database.SaveChangesAsync();
          onSave?.Invoke(item);
          return Results.Created($"{item.Id}", request.Mapper.Map<TDestination>(item));
        } catch (Exception e) {
          Console.WriteLine(e);
          return Results.Problem(e.Message, statusCode: StatusCodes.Status400BadRequest);
        }
      }).ProducesValidationProblem()
      .Produces<TDestination>(StatusCodes.Status201Created);
  }

  public RouteHandlerBuilder MapUpdate<TRequest, TBody>(QueryGeneratorDelegate<TRequest> queryGenerator, ObjectUpdateDelegate<TRequest> mapper)
    where TRequest : UpdateRequest<TKey, TBody> {
    return _root.MapPut(_pathId, async ([AsParameters] TRequest request) => {
        var item = await queryGenerator.Invoke(request)
          .Where(u => u.Id.Equals(request.Id))
          .FirstOrDefaultAsync();

        if (item is null) {
          return Results.Problem("Not found", statusCode: StatusCodes.Status404NotFound);
        }

        try {
          mapper.Invoke(request, item, out var onSave);
          
          await request.Database.SaveChangesAsync();

          onSave?.Invoke(item);
          return Results.NoContent();
        } catch (Exception e) {
          Console.WriteLine(e);
          return Results.Problem(e.Message, statusCode: StatusCodes.Status400BadRequest);
        }
      }).Produces(StatusCodes.Status204NoContent)
      .ProducesValidationProblem()
      .ProducesProblem(StatusCodes.Status404NotFound);
  }

  public RouteHandlerBuilder MapDelete<TRequest>(QueryGeneratorDelegate<TRequest> queryGenerator)
    where TRequest : KeyRequest<TKey> {
    return _root.MapDelete(_pathId, async ([AsParameters] TRequest request) => {
        var item = await queryGenerator.Invoke(request)
          .Where(u => u.Id.Equals(request.Id))
          .FirstOrDefaultAsync();

        if (item is null) {
          return Results.Problem("Not found", statusCode: StatusCodes.Status404NotFound);
        }

        try {
          item.Deleted = true;
          await request.Database.SaveChangesAsync();
          return Results.NoContent();
        } catch (Exception e) {
          Console.WriteLine(e);
          return Results.Problem(e.Message, statusCode: StatusCodes.Status400BadRequest);
        }
      }).Produces(StatusCodes.Status204NoContent)
      .ProducesProblem(StatusCodes.Status404NotFound);
  }
}
