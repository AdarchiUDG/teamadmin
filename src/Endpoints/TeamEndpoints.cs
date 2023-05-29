using Microsoft.EntityFrameworkCore;
using Prometheus.Data.Entities;
using Prometheus.Models.Team;
using Prometheus.Models.Requests;

namespace Prometheus.Endpoints; 

public static class TeamEndpoints {
  public static IEndpointRouteBuilder AddTeamEndpoints(this IEndpointRouteBuilder builder) {
    var crudEndpoints = new CrudEndpointBuilder<Team, int, TeamResponse>(builder);
    crudEndpoints.MapPaged<ListRequest>(QueryGenerator)
      .WithSwagger("List Teams", "Gets the list of all teams");
    crudEndpoints.MapGetById<KeyRequest<int>>(QueryGenerator)
      .WithSwagger("Get Team By Id", "Gets a team by its ID");
    crudEndpoints.MapDelete<KeyRequest<int>>(QueryGenerator)
      .WithSwagger("Delete Team", "Deletes a team by its ID");

    return builder;
  }

  public static IEndpointRouteBuilder AddUserTeamEndpoints(this IEndpointRouteBuilder builder) {
    var crudEndpoints = new CrudEndpointBuilder<Team, int, TeamResponse>(builder);
    crudEndpoints.MapCreate<BodyRequest<TeamPayload>>(Mapper)
      .WithSwagger("Create Team", "Creates a team assigned to a user");
    crudEndpoints.MapUpdate<UpdateRequest<int, TeamPayload>, TeamPayload>(request => QueryGenerator(request).Include(t => t.Members), ObjectUpdateDelegate)
      .WithSwagger("Update Team", "Updates a team");

    return builder;
  }

  private static void ObjectUpdateDelegate(UpdateRequest<int, TeamPayload> request, Team item, out Action<Team>? onSave) {
    onSave = null;

    var members = request.Database.Children
      .Where(c => request.Payload.Members.Contains(c.Id))
      .ToList();
    
    item.Name = request.Payload.Name;
    item.Trainer = request.GetValue<User>("user")!;
    item.Members = members;
  }

  private static Team Mapper(BodyRequest<TeamPayload> request, out Action<Team>? onSave) {
    onSave = null;
    
    return new Team {
      Name = request.Payload.Name,
      Trainer = request.GetValue<User>("user")!,
      Members = request.Database.Children
        .Where(c => request.Payload.Members.Contains(c.Id))
        .ToList()
    };
  }
  private static IQueryable<Team> QueryGenerator(Request context) {
    var user = context.GetValue<User>("user");
    var query = context.Database.Teams
      .Where(u => !u.Deleted);

    if (user is not null) {
      query = query.Where(u => u.Trainer.Id == user.Id);
    }

    return query;
  }
}
