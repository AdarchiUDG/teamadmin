namespace Prometheus.Data.Entities; 

public interface IDeletableEntity {
  bool Deleted { get; set; }
}
