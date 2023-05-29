using Prometheus.Models.User;

namespace Prometheus.Models.Team; 

public class TeamResponse : BasicTeamResponse {
  public int TotalMembers { get; set; }
  public int TotalMatches { get; set; }
}
