using Expo.Server.Client;
using Prometheus.Services;

namespace Prometheus.ServiceExtensions; 

public static class NotificationExtensions {
  public static IServiceCollection AddNotifications(this IServiceCollection collection) {
    collection.AddSingleton<NotificationService>();
    collection.AddHostedService(provider => provider.GetRequiredService<NotificationService>());
    
    return collection;
  }
}
