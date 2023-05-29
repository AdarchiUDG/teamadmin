using Microsoft.EntityFrameworkCore;
using Prometheus.Data.Entities;
using Prometheus.Models.Announcement;
using Prometheus.Models.Requests;

namespace Prometheus.Endpoints; 

public static class AnnouncementEndpoints {
  public static IEndpointRouteBuilder AddAnnouncementEndpoints(this IEndpointRouteBuilder builder) {
    var crudEndpoints = new CrudEndpointBuilder<Announcement, int, AnnouncementResponse>(builder);
    crudEndpoints.MapPaged<ListRequest>(QueryGenerator)
      .WithSwagger("List Announcements", "Gets the list of all announcements");
    crudEndpoints.MapGetById<KeyRequest<int>>(QueryGenerator)
      .WithSwagger("Get Announcement By Id", "Gets a announcement by its ID");
    crudEndpoints.MapDelete<KeyRequest<int>>(QueryGenerator)
      .WithSwagger("Delete Announcement", "Deletes a announcement by its ID");

    return builder;
  }

  public static IEndpointRouteBuilder AddUserAnnouncementEndpoints(this IEndpointRouteBuilder builder) {
    var crudEndpoints = new CrudEndpointBuilder<Announcement, int, AnnouncementResponse>(builder);
    crudEndpoints.MapCreate(
      (BodyRequest<AnnouncementPayload> request, out Action<Announcement>? onSave) => {
        onSave = (announcement) => {
          request.Notifications.Create(new Notification() {
            Title = $"Anuncio: {announcement.Title}",
            Content = announcement.Content.Length > 80 ? announcement.Content[..80] : announcement.Content,
            ScheduledAt = request.Payload.ScheduledAt,
            MetaData = new NotificationMetaData {
              Key = announcement.Id.ToString(),
              Type = NotificationType.Announcement
            },
            Targets = request.Database.Roles.Where(r => r.Id == 3)
              .SelectMany(r => r.Users)
              .ToList()
          }, request.Database).Wait();
        };
        return new Announcement() {
          Title = request.Payload.Title,
          Content = request.Payload.Content,
          CreatedBy = request.GetValue<User>("user")!,
          ScheduledAt = request.Payload.ScheduledAt
        };
      }).WithSwagger("Create Announcement", "Creates an announcement published by the user");

    crudEndpoints.MapUpdate<UpdateRequest<int, AnnouncementPayload>, AnnouncementPayload>(QueryGenerator, (UpdateRequest<int, AnnouncementPayload> request, Announcement item,
      out Action<Announcement>? onSave) => {
      onSave = null;
      item.Title = request.Payload.Title;
      item.Content = request.Payload.Content;
    }).WithSwagger("Update Announcement", "Updates an announcement published by the user");

    return builder;
  }
  
  private static IQueryable<Announcement> QueryGenerator(Request context) {
    var query = context.Database.Announcements
      .Where(u => !u.Deleted);

    var user = context.GetValue<User>("user");

    query = user is not null ? query.Where(u => u.CreatedBy.Id == user.Id) : query.Where(u => u.ScheduledAt <= DateTime.UtcNow);

    return query.OrderByDescending(a => a.ScheduledAt);
  }
}
