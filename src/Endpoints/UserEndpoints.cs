using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Prometheus.Data.Entities;
using Prometheus.Models.Requests;
using Prometheus.Models.User;
using Prometheus.Security;

namespace Prometheus.Endpoints;

public static class UserEndpoints {
  public static IEndpointRouteBuilder AddUserEndpoints(this IEndpointRouteBuilder builder) {
    var crudEndpoints = new CrudEndpointBuilder<User, Guid, UserResponse>(builder);

    builder.MapGet("@me", async ([AsParameters] Request request) => {
      var userId = request.GetValue<Guid>("currentUserId");

      return TypedResults.Ok(await QueryGenerator(request)
        .Where(u => u.Id == userId)
        .ProjectTo<UserResponse>(request.MapperConfig)
        .FirstOrDefaultAsync());
      }).WithSwagger("Get Current User", "Gets the logged in user")
      .RequireAuthorization("general");
    
    crudEndpoints.MapPaged<ListRequest>(QueryGenerator)
      .WithSwagger("List Users", "Gets the list of all users");
    crudEndpoints.MapGetById<KeyRequest<Guid>>(QueryGenerator)
      .WithSwagger("Get User By Id", "Gets a user by its GUID");
    crudEndpoints.MapCreate<BodyRequest<UserPayload>>(Mapper)
      .WithSwagger("Create User", "Creates a user given some information");
    crudEndpoints.MapUpdate<UpdateRequest<Guid, UserPayload>, UserPayload>((request) => QueryGenerator(request).Include(u => u.Roles), ObjectUpdateDelegate)
      .WithSwagger("Update User", "Updates an user given some information");
    crudEndpoints.MapDelete<KeyRequest<Guid>>(QueryGenerator)
      .WithSwagger("Delete User", "Deletes a user by its GUID");
    
    

    return builder;
  }

  public static void AddParentEndpoints(this IEndpointRouteBuilder builder) {
    var crudEndpoints = new CrudEndpointBuilder<User, Guid, UserResponse>(builder);
    crudEndpoints.MapPaged<ListRequest>(ParentQueryGenerator)
      .WithSwagger("List Parents", "Gets the list of users with the Parent role");
  }

  private static void ObjectUpdateDelegate(UpdateRequest<Guid, UserPayload> request, User item,
    out Action<User>? onSave) {
    onSave = null;

    if (!request.User.IsInRole("administrator") && request.Payload.Roles.Any(r => r < 3)) {
      throw new UnauthorizedAccessException("You do not have permission to update a user with this role");
    }

    item.Name = request.Payload.Name;
    item.LastName = request.Payload.LastName;
    item.Email = request.Payload.Email;
    item.Phone = request.Payload.Phone;
    item.Roles = request.Database.Roles.Where(r => request.Payload.Roles.Contains(r.Id)).ToList();
  }

  private static User Mapper(BodyRequest<UserPayload> request, out Action<User>? onSave) {
    var password = Crypto.GeneratePassword(length: 8);
    
    if (!request.User.IsInRole("administrator") && request.Payload.Roles.Any(r => r < 3)) {
      throw new UnauthorizedAccessException("You do not have permission to create a user with this role");
    }

    var userInDb = request.Database.Users
      .Include(u => u.Roles)
      .FirstOrDefault(u => EF.Functions.ILike(u.Email, request.Payload.Email) && u.Deleted);

    if (userInDb is not null) {
      onSave = null;

      userInDb.Name = request.Payload.Name;
      userInDb.LastName = request.Payload.LastName;
      userInDb.Email = request.Payload.Email;
      userInDb.Phone = request.Payload.Phone;
      userInDb.Roles = request.Database.Roles.Where(r => request.Payload.Roles.Contains(r.Id)).ToList();
      
      return userInDb;
    }
    
    onSave = user => {
      request.Mail.SendWelcomeEmail(user, password)
        .Wait();
    };

    return new User {
      Name = request.Payload.Name,
      LastName = request.Payload.LastName,
      Email = request.Payload.Email,
      Password = Crypto.HashPassword(password),
      Phone = request.Payload.Phone,
      Roles = request.Database.Roles.Where(r => request.Payload.Roles.Contains(r.Id)).ToList()
    };
  }
  private static IQueryable<User> QueryGenerator(Request context) {
    var query = context.Database.Users
      .AsSplitQuery()
      .Where(u => !u.Deleted);

    return query;
  }
  private static IQueryable<User> ParentQueryGenerator(Request context) {
    var query = QueryGenerator(context)
      .Where(u => u.Roles.Any(r => r.Id == 3));

    return query;
  }
}
