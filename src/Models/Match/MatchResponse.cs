using Prometheus.Models.Team;

namespace Prometheus.Models.Match; 

public class MatchResponse {
  public int Id { get; set; }
  public int Round { get; set; }
  public DateTime StartDate { get; set; } 
  public BasicTeamResponse FirstTeam { get; set; }
  public int FirstTeamScore { get; set; }
  public BasicTeamResponse SecondTeam { get; set; }
  public int SecondTeamScore { get; set; }
  public BasicTeamResponse? WinningTeam => FirstTeamScore == 0 && SecondTeamScore == 0 ? null : FirstTeamScore > SecondTeamScore ? FirstTeam : SecondTeam;
}
