namespace Prometheus.Data.Entities; 

public abstract class BaseEntity<TKey> : ITimedEntity, IDeletableEntity {
  public TKey Id { get; set; }
  public bool Deleted { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
