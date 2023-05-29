using Prometheus.Models.User;

namespace Prometheus.Models.Announcement; 

public class AnnouncementResponse {
  public int Id { get; set; }
  public string Title { get; set; }
  public string Content { get; set; }
  public DateTime ScheduledAt { get; set; }
  public BasicUserResponse PostedBy { get; set; }
}
