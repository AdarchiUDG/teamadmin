namespace Prometheus.Data.Entities; 

public class RefreshToken : BaseEntity<int> {
  public string Token { get; set; }
  public User User { get; set; }
}
