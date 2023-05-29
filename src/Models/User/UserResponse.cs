namespace Prometheus.Models.User; 

public class UserResponse : BasicUserResponse {
  public List<string> Roles { get; set; }
  
  public int TotalChildren { get; set; }
  public int TotalPayments { get; set; }
}
