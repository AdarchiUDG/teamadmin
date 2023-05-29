namespace Prometheus.Data.Entities; 

public class Announcement : BaseEntity<int> {
  public required string Title { get; set; }
  public required string Content { get; set; }
  public required DateTime ScheduledAt { get; set; }
  public required User CreatedBy { get; set; }
}
