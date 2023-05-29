namespace Prometheus.Data.Entities; 

public class Match : BaseEntity<int> {
  public required DateTime StartDate { get; set; }
  public int FirstTeamId { get; set; }
  public int FirstTeamScore { get; set; }
  public Team FirstTeam { get; set; }
  public int SecondTeamId { get; set; }
  public int SecondTeamScore { get; set; }
  public Team SecondTeam { get; set; }
}
