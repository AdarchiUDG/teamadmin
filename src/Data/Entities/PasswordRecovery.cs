namespace Prometheus.Data.Entities; 

public class PasswordRecovery : BaseEntity<int> {
  public required string Code { get; set; }
  public required User User { get; set; }
  public required DateTime ExpiresAt { get; set; }
}
