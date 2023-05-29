namespace Prometheus.Models.Announcement; 

public class AnnouncementPayload {
  public string Title { get; set; }
  public string Content { get; set; }
  public DateTime ScheduledAt { get; set; }
}
