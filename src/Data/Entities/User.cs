namespace Prometheus.Data.Entities; 

public class User : BaseEntity<Guid> {
  public string Name { get; set; }
  public string LastName { get; set; }
  public string Email { get; set; }
  public string Password { get; set; }
  public string Phone { get; set; }
  public ICollection<Role> Roles { get; set; } = new HashSet<Role>();
  public ICollection<Team> Teams { get; set; } = new HashSet<Team>();
  public ICollection<Child> Children { get; set; } = new HashSet<Child>();
  public ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>();
  public ICollection<PasswordRecovery> PasswordRecoveries { get; set; } = new HashSet<PasswordRecovery>();
  public ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
  public ICollection<PushToken> PushTokens { get; set; } = new HashSet<PushToken>();
  public ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();
}
