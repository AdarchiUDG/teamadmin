namespace Prometheus.Models.Child; 

public class ChildPayload {
  public string Name { get; set; }
  public string LastName { get; set; }
  public DateOnly BirthDate { get; set; }
}
