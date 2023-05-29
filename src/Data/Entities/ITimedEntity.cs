namespace Prometheus.Data.Entities; 

public interface ITimedEntity {
  DateTime CreatedAt { get; set; }
  DateTime UpdatedAt { get; set; }
}
