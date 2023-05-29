namespace Prometheus.Data.Entities; 

public class PushToken {
  public required string Token { get; set; }
  public required Guid UserId { get; set; }
  public User User { get; set; }
}
