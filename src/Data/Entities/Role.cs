namespace Prometheus.Data.Entities; 

public class Role : BaseEntity<int> {
  public required string Slug { get; set; }
  public ICollection<User> Users { get; set; } = new HashSet<User>();
}
