using Prometheus.Data.Entities;

namespace Prometheus.Models.Notifications; 

public class NotificationResponse {
  public string Title { get; set; }
  public string Content { get; set; }
  public DateTime ScheduledAt { get; set; }
  public NotificationMetaData? MetaData { get; set; }
  public bool Read { get; set; }
}
