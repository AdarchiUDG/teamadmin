using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Prometheus.Data.Entities;
using Prometheus.Models.Requests;
using Swashbuckle.AspNetCore.Annotations;

namespace Prometheus.Endpoints; 

public static class EndpointExtensions {
  public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder) {
    var userEndpoints = builder.MapGroup("user")
      .WithTags("Users")
      .RequireAuthorization("general")
      .AddUserEndpoints();
    
    builder.MapGroup("parent")
      .WithTags("Users")
      .RequireAuthorization("general")
      .AddParentEndpoints();

    userEndpoints.MapGroup("@me/notifications")
      .WithTags("User Notifications")
      .RequireAuthorization("general")
      .AddNotificationEndpoints();
    userEndpoints.MapGroup("{userId:guid}/children")
      .WithTags("User Children")
      .RequireAuthorization("general")
      .WithParameter("userId", format: "uuid")
      .AddEndpointFilter(UserEndpointFilter)
      .AddChildListEndpoint()
      .AddChildEndpoints();
    userEndpoints.MapGroup("{userId:guid}/payments")
      .WithTags("User Payments")
      .RequireAuthorization("general")
      .WithParameter("userId", format: "uuid")
      .AddEndpointFilter(UserEndpointFilter)
      .AddPaymentEndpoints();
    userEndpoints.MapGroup("{userId:guid}/announcements")
      .WithTags("User Announcements")
      .RequireAuthorization("general")
      .WithParameter("userId", format: "uuid")
      .AddEndpointFilter(UserEndpointFilter)
      .AddAnnouncementEndpoints()
      .AddUserAnnouncementEndpoints();
    userEndpoints.MapGroup("{userId:guid}/teams")
      .WithTags("User Teams")
      .RequireAuthorization("general")
      .WithParameter("userId", format: "uuid")
      .AddEndpointFilter(UserEndpointFilter)
      .AddTeamEndpoints()
      .AddUserTeamEndpoints();

    builder.MapGroup("announcements")
      .WithTags("Announcements")
      .RequireAuthorization("general")
      .AddAnnouncementEndpoints();

    builder.MapGroup("children")
      .WithTags("Children")
      .RequireAuthorization("general")
      .AddChildListEndpoint();

    var teams = builder.MapGroup("teams")
      .WithTags("Teams")
      .RequireAuthorization("general")
      .AddTeamEndpoints();

    teams.MapGroup("{teamId:int}/matches")
      .WithTags("Teams", "Team Matches")
      .RequireAuthorization("general")
      .WithParameter("teamId", type: "integer", format: "int32")
      .AddEndpointFilter(TeamEndpointFilter)
      .AddMatchEndpoints();

    teams.MapGroup("{teamId:int}/members")
      .WithTags("Teams", "Team Members")
      .RequireAuthorization("general")
      .WithParameter("teamId", type: "integer", format: "int32")
      .AddEndpointFilter(TeamEndpointFilter)
      .AddChildListEndpoint();

    builder.MapGroup("matches")
      .WithTags("Matches")
      .RequireAuthorization("general")
      .AddMatchEndpoints();

    builder.MapGroup("auth")
      .WithTags("Authentication")
      .AddAuthenticationEndpoints();
    return builder;
  }
  private static async ValueTask<object?> UserEndpointFilter(EndpointFilterInvocationContext context, EndpointFilterDelegate next) {
    Request? request = null;

    foreach (var argument in context.Arguments) {
      if (argument is not Request req) continue;
      request = req;
      break;
    }

    if (request is null) return await next(context);

    request.TryGetRoute("userId", out var userId);
    var guid = Guid.Parse(userId as string ?? string.Empty);

    var user = await request.Database.Users.Where(u => !u.Deleted)
      .FirstOrDefaultAsync(u => u.Id == guid);

    if (user is null) {
      return Results.NotFound();
    }

    request.AddValue("user", user);

    return await next(context);
  }
  
  private static async ValueTask<object?> TeamEndpointFilter(EndpointFilterInvocationContext context, EndpointFilterDelegate next) {
    Request? request = null;

    foreach (var argument in context.Arguments) {
      if (argument is not Request req) continue;
      request = req;
      break;
    }

    if (request is null) return await next(context);

    request.TryGetRoute("teamId", out var teamId);
    var id = int.Parse(teamId as string ?? string.Empty);

    var team = await request.Database.Teams.Where(u => !u.Deleted)
      .FirstOrDefaultAsync(t => t.Id == id);

    if (team is null) {
      return Results.NotFound();
    }

    request.AddValue("team", team);

    return await next(context);
  }

  public static TBuilder WithParameter<TBuilder>(this TBuilder builder, string name, string type = "string", 
    string format = "") where TBuilder : IEndpointConventionBuilder {
    builder.WithMetadata(new OpenApiParameter() {
      Name = name,
      Required = true,
      In = ParameterLocation.Path,
      Schema = new OpenApiSchema() {
        Type = type,
        Format = format
      }
    });
    
    return builder;
  }

  public static TBuilder WithSwagger<TBuilder>(this TBuilder builder, string summary, string description) where TBuilder : IEndpointConventionBuilder {
    builder.WithMetadata(new SwaggerOperationAttribute(summary, description));
    return builder;
  }
}
