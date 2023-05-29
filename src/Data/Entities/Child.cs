namespace Prometheus.Data.Entities; 

public class Child : BaseEntity<int> {
  public required string Name { get; set; }
  public required string LastName { get; set; }
  public required DateOnly BirthDate { get; set; }
  public required User Parent { get; set; }
  public required Team? Team { get; set; }
}
