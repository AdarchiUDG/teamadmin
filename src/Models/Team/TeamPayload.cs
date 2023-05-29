namespace Prometheus.Models.Team; 

public class TeamPayload {
  public string Name { get; set; }
  public Guid TrainerId { get; set; }
  public int[] Members { get; set; }
}
