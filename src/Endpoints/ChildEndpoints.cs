using Microsoft.AspNetCore.Mvc;
using Prometheus.Data.Entities;
using Prometheus.Models.Child;
using Prometheus.Models.Requests;
using Prometheus.Models.User;
using Prometheus.Security;

namespace Prometheus.Endpoints; 

public static class ChildEndpoints {
  public static IEndpointRouteBuilder AddChildEndpoints(this IEndpointRouteBuilder builder) {
    var crudEndpoints = new CrudEndpointBuilder<Child, int, ChildResponse>(builder);
    crudEndpoints.MapGetById<KeyRequest<int>>(QueryGenerator)
      .WithSwagger("Get Child By Id", "Gets a child by its id");
    crudEndpoints.MapCreate<BodyRequest<ChildPayload>>(Mapper)
      .WithSwagger("Create Child", "Creates a child give some information");
    crudEndpoints.MapUpdate<UpdateRequest<int, ChildPayload>, ChildPayload>(QueryGenerator, ObjectUpdateDelegate)
      .WithSwagger("Update Child", "Updates the information of a child");
    crudEndpoints.MapDelete<KeyRequest<int>>(QueryGenerator)
      .WithSwagger("Delete Child", "Deletes a child by its id");

    return builder;
  }

  public static IEndpointRouteBuilder AddChildListEndpoint(this IEndpointRouteBuilder builder) {
    var crudEndpoints = new CrudEndpointBuilder<Child, int, ChildResponse>(builder);
    crudEndpoints.MapPaged<ListRequest>(QueryGenerator)
      .WithSwagger("List Children", "Gets the list of all children");

    return builder;
  }

  private static void ObjectUpdateDelegate(UpdateRequest<int, ChildPayload> request, Child item, out Action<Child>? onSave) {
    onSave = null;

    item.Name = request.Payload.Name;
    item.LastName = request.Payload.LastName;
    item.BirthDate = request.Payload.BirthDate;
  }

  private static Child Mapper(BodyRequest<ChildPayload> request, out Action<Child>? onSave) {
    onSave = null;
    return new Child() {
      Name = request.Payload.Name,
      LastName = request.Payload.LastName,
      BirthDate = request.Payload.BirthDate,
      Parent = request.GetValue<User>("user")!,
      Team = null
    };
  }
  private static IQueryable<Child> QueryGenerator(Request context) {
    var user = context.GetValue<User>("user");
    var team = context.GetValue<Team>("team");
    var query = context.Database.Children
      .Where(u => !u.Deleted);

    if (user is not null) {
      query = query.Where(u => u.Parent.Id == user.Id);
    }

    if (team is not null) {
      query = query.Where(u => u.Team != null && u.Team.Id == team.Id);
    }

    return query;
  }
}
