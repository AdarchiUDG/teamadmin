using System.Security.Claims;
using AutoMapper;
using Prometheus.Data;
using Prometheus.Services;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace Prometheus.Models.Requests;

public class Request {
  public required ClaimsPrincipal User { get; set; }
  public required DatabaseContext Database { get; set; }
  public required IMapper Mapper { get; set; }
  public required HttpRequest HttpRequest { get; set; }
  public required MailService Mail { get; set; }
  public required NotificationService Notifications { get; set; }

  public T? GetValue<T>(string key) {
    HttpRequest.HttpContext.Items.TryGetValue(key, out var tmp);

    return tmp is T value ? value : default;
  }

  public void AddValue(string key, object value) {
    HttpRequest.HttpContext.Items.Add(key, value);
  }

  public bool TryGetRoute(string key, out object? value) => HttpRequest.RouteValues.TryGetValue(key, out value);

  public IConfigurationProvider MapperConfig => Mapper.ConfigurationProvider;
}
