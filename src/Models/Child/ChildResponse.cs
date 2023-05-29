using Prometheus.Models.Team;

namespace Prometheus.Models.Child; 

public class ChildResponse {
  public int Id { get; set; }
  public string Name { get; set; }
  public string LastName { get; set; }
  public int? TeamId { get; set; }
  public string? TeamName { get; set; }
  public DateOnly BirthDate { get; set; }
}
