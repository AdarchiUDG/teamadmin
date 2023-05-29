namespace Prometheus.Data.Entities; 

public class Notification : BaseEntity<int> {
  public required string Title { get; set; }
  public required string Content { get; set; }
  public required DateTime ScheduledAt { get; set; }
  public bool Read { get; set; }
  public bool Sent { get; set; }
  public NotificationMetaData? MetaData { get; set; }
  public ICollection<User> Targets { get; set; } = new HashSet<User>();
}

public class NotificationMetaData {
  public required string Key { get; set; }
  public required NotificationType Type { get; set; }
  public string Subtitle => Type switch {
    NotificationType.Announcement => "Anuncio",
    NotificationType.Match => "Encuentro",
    NotificationType.MatchResult => "Resultado de Encuentro",
    NotificationType.Payment => "Pago",
    NotificationType.PaymentVoucher => "Voucher de Pago",
    _ => ""
  };
}

public enum NotificationType {
  Announcement,
  Match,
  MatchResult,
  Payment,
  PaymentVoucher
}
