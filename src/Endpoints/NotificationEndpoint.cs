using Microsoft.EntityFrameworkCore;
using Prometheus.Data.Entities;
using Prometheus.Models;
using Prometheus.Models.Notifications;
using Prometheus.Models.Requests;

namespace Prometheus.Endpoints; 

public static class NotificationEndpoint {
  public static IEndpointRouteBuilder AddNotificationEndpoints(this IEndpointRouteBuilder builder) {
    var crudEndpoints = new CrudEndpointBuilder<Notification, int, NotificationResponse>(builder);
    crudEndpoints.MapPaged<ListRequest>(Query)
      .WithSwagger("List Notifications", "Gets current user's notifications");
    builder.MapGet("unread", async ([AsParameters] Request request) => {
        var total = await Query(request).CountAsync(n => !n.Read);
        return new CountResponse() {
          Count = total
        };
      }).WithSwagger("Unread Notifications", "Gets total unread notifications");
    builder.MapPatch("read", async ([AsParameters] Request request) => {
      await Query(request)
        .Where(n => n.Read)
        .ExecuteUpdateAsync(
          notification => notification.SetProperty(n => n.Read, true));

      Console.WriteLine("Test");
      return TypedResults.NoContent();
    }).WithSwagger("Mark As Read", "Marks all past notifications as read, leaves those not launched yet as read");
    
    builder.MapPost("token", async ([AsParameters] BodyRequest<PushTokenPayload> request) => {
      var userId = request.GetValue<Guid>("currentUserId");
      var token = await request.Database.PushTokens
        .Where(pt => pt.Token == request.Payload.Token)
        .FirstOrDefaultAsync();

      if (token is not null) {
        if (token.UserId == userId) {
          return TypedResults.NoContent();
        }

        request.Database.Remove(token);
      } else {
        await request.Database.AddAsync(new PushToken {
          Token = request.Payload.Token,
          UserId = userId
        });
      }
      
      Console.WriteLine(new { userId, request.Payload.Token });

      await request.Database.SaveChangesAsync();
      
      return TypedResults.NoContent();
    }).WithSwagger("Add Push Token", "Assigns a push token to the current user");
    return builder;
  }

  private static IQueryable<Notification> Query(Request context) {
    var id = context.GetValue<Guid>("currentUserId")!;
    return context.Database.Users
      .Where(u => u.Id == id)
      .SelectMany(u => u.Notifications)
      .Where(n => DateTime.UtcNow >= n.ScheduledAt)
      .OrderByDescending(n => n.ScheduledAt);
  }
}
