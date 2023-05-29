namespace Prometheus.Models.Match; 

public class MatchPayload {
  public DateTime StartDate { get; set; }
  public int FirstTeamId { get; set; }
  public int FirstTeamScore { get; set; }
  public int SecondTeamId { get; set; }
  public int SecondTeamScore { get; set; }
}
