using Prometheus.Models.User;

namespace Prometheus.Models.Team; 

public class BasicTeamResponse {
  public int Id { get; set; }
  public string Name { get; set; }
  public BasicUserResponse Trainer { get; set; } 
}
