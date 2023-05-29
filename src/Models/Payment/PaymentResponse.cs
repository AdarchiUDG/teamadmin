using Prometheus.Models.User;

namespace Prometheus.Models.Payment; 

public class PaymentResponse {
  public Guid Id { get; set; }
  public string Name { get; set; }
  public decimal Amount { get; set; }
  public bool Paid { get; set; }
  public bool RequestedVoucher { get; set; }
  public bool HasVoucher { get; set; }
  public DateTime? PaidAt { get; set; }
  public UserResponse User { get; set; }
  public DateTime IssuedAt { get; set; }
}
