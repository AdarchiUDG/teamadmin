namespace Prometheus.Data.Entities; 

public class Team : BaseEntity<int> {
  public required string Name { get; set; }
  public required User Trainer { get; set; }
  public ICollection<Child> Members { get; set; } = new HashSet<Child>();
  public ICollection<Match> FirstTeamMatches { get; set; } = new HashSet<Match>();
  public ICollection<Match> SecondTeamMatches { get; set; } = new HashSet<Match>();
}
