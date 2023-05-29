using Microsoft.EntityFrameworkCore;
using Prometheus.Data.Entities;
using Prometheus.Models.Match;
using Prometheus.Models.Requests;

namespace Prometheus.Endpoints; 

public static class MatchEndpoints {
  public static IEndpointRouteBuilder AddMatchEndpoints(this IEndpointRouteBuilder builder) {
    var crudEndpoints = new CrudEndpointBuilder<Match, int, MatchResponse>(builder);
    crudEndpoints.MapPaged<ListRequest>(QueryGenerator)
      .WithSwagger("List Matches", "Gets the list of all matches");
    crudEndpoints.MapGetById<KeyRequest<int>>(QueryGenerator)
      .WithSwagger("Get Match By Id", "Gets a match by its ID");
    crudEndpoints.MapCreate<BodyRequest<MatchPayload>>(Mapper)
      .WithSwagger("Create Match", "Creates a match assigned to a tournament");
    crudEndpoints.MapUpdate<UpdateRequest<int, MatchPayload>, MatchPayload>(QueryGenerator, ObjectUpdateDelegate)
      .WithSwagger("Update Match", "Updates a tournament");
    crudEndpoints.MapDelete<KeyRequest<int>>(QueryGenerator)
      .WithSwagger("Delete Match", "Deletes a match by its ID");

    return builder;
  }

  private static void ObjectUpdateDelegate(UpdateRequest<int, MatchPayload> request, Match item, out Action<Match>? onSave) {
    var hasGameEnded = request.Payload.StartDate <= DateTime.UtcNow;
    onSave = (match) => {
      if (!hasGameEnded) return;
      
      
    };

    item.FirstTeamId = request.Payload.FirstTeamId;
    item.SecondTeamId = request.Payload.SecondTeamId;
    item.StartDate = request.Payload.StartDate;
    item.FirstTeamScore = hasGameEnded ? request.Payload.FirstTeamScore : 0;
    item.SecondTeamScore = hasGameEnded ? request.Payload.SecondTeamScore : 0;
  }

  private static Match Mapper(BodyRequest<MatchPayload> request, out Action<Match>? onSave) {
    var hasGameEnded = request.Payload.StartDate <= DateTime.UtcNow;

    onSave = (match) => {
      request.Notifications.Create(new Notification() {
        Title = $"Nuevo encuentro",
        Content = $"{match.FirstTeam.Name} vs {match.SecondTeam.Name}",
        ScheduledAt = match.StartDate,
        MetaData = new NotificationMetaData {
          Key = match.Id.ToString(),
          Type = NotificationType.Match
        },
        Targets = request.Database.Users
          .Where(u =>
            u.Children.Any(c =>
              c.Team != null && (c.Team.Id == match.FirstTeamId || c.Team.Id == match.SecondTeamId)))
          .ToList()
      }, request.Database).Wait();
    };

    var firstTeam = request.Database.Teams.FirstOrDefault(t => t.Id == request.Payload.FirstTeamId);
    var secondTeam = request.Database.Teams.FirstOrDefault(t => t.Id == request.Payload.SecondTeamId);

    if (firstTeam is null || secondTeam is null) {
      throw new BadHttpRequestException("Teams not found");
    }

    return new Match {
      StartDate = request.Payload.StartDate,
      FirstTeam = firstTeam,
      SecondTeam = secondTeam,
      FirstTeamScore = hasGameEnded ? request.Payload.FirstTeamScore : 0,
      SecondTeamScore = hasGameEnded ? request.Payload.SecondTeamScore : 0,
    };
  }
  private static IQueryable<Match> QueryGenerator(Request context) {
    var team = context.GetValue<Team>("team");
    var query = context.Database.Matches
      .Where(u => !u.Deleted);

    if (team is not null) {
      query = query.Where(q => q.FirstTeamId == team.Id || q.SecondTeamId == team.Id);
    }

    return query.OrderByDescending(q => q.StartDate);
  }
}
