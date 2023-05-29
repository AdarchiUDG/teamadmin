namespace Prometheus.Data.Entities; 

public class Payment : BaseEntity<Guid> {
  public required string Name { get; set; }
  public required decimal Amount { get; set; }
  public bool Paid { get; set; }
  public bool RequestedVoucher { get; set; }
  public bool HasVoucher { get; set; }
  public required User User { get; set; }
  public DateTime? PaidAt { get; set; }
}
