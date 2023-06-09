namespace Prometheus.Models.Auth; 

public class ResetPasswordPayload {
  public required string Email { get; set; }
  public required string Password { get; set; }
  public required string Code { get; set; }
}
