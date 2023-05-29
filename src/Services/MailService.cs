using System.Net.Mail;
using Prometheus.Data.Entities;

namespace Prometheus.Services; 

public class MailService {
  private readonly SmtpClient _smtpClient;
  private readonly MailAddress _from = new MailAddress(address: "no-reply@cbnao.com", displayName: "TeamAdmin");
  public MailService(SmtpClient smtpClient) {
    _smtpClient = smtpClient;
  }
  public async Task SendWelcomeEmail(User user, string password) {
    var msg = new MailMessage() {
      From = _from,
      To = {
        new MailAddress(address: user.Email, displayName: $"{user.Name} {user.LastName}")
      },
      Body = $"Bienvenido {user.Name} {user.LastName}<br><br>Tu contraseña es <b>{password}</b>",
      Subject = "Bienvenido a TeamAdmin",
      IsBodyHtml = true
    };

    await Send(msg);
  }

  public async Task SendRecoveryEmail(User user, string code) {
    var msg = new MailMessage() {
      From = _from,
      To = {
        new MailAddress(address: user.Email, displayName: $"{user.Name} {user.LastName}")
      },
      Body = $"Se realizo una solicitud de recuperación de contraseña.<br>Si tu no solicitaste esto haz caso omiso a este mensaje.<br>El código para recuperar tu contraseña es:<br><br><b>{code}</b><br><br>Este código será valido por 15 minutos.",
      Subject = "TeamAdmin - Solicitud de recuperación de contraseña",
      IsBodyHtml = true
    };

    await Send(msg);
  }

  public async Task Send(MailMessage message) {
    await Task.Run(() => {
      _smtpClient.Send(message);
    });
  }
}
