namespace Prometheus.Models.Auth; 

public class ChangePasswordPayload {
  public required string OldPassword { get; set; }
  public required string NewPassword { get; set; }
}
