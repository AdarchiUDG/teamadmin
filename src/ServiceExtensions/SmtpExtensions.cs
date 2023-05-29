using System.Net;
using System.Net.Mail;
using Prometheus.Services;

namespace Prometheus.ServiceExtensions; 

public static class SmtpExtensions {
  public static IServiceCollection AddSmtp(this IServiceCollection collection) {
    collection.AddSingleton(new SmtpClient(
      host: Environment.GetEnvironmentVariable("SMTP_HOST"), 
      port: int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT")!)) {
      EnableSsl = true,
      UseDefaultCredentials = false,
      Credentials = new NetworkCredential(
        userName: Environment.GetEnvironmentVariable("SMTP_EMAIL"), 
        password: Environment.GetEnvironmentVariable("SMTP_PASSWORD"))
    });

    collection.AddSingleton<MailService>();

    return collection;
  }
}
