using System.Text.Json;
using System.Text.Json.Serialization;

namespace Prometheus.ServiceExtensions; 

public static class RoutingExtensions {
  public static IServiceCollection ConfigureRouting(this IServiceCollection services) {
    services.AddRouting(c => {
      c.LowercaseUrls = true;
      c.LowercaseQueryStrings = true;
    });
    
    services.ConfigureHttpJsonOptions(options => {
      options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
      options.SerializerOptions.Converters.Add(new TrimmingConverter());
      options.SerializerOptions.IncludeFields = true;
    });
    
    services.AddControllers();

    return services;
  } 
  
  private class TrimmingConverter : JsonConverter<string>
  {
    public override string Read(
      ref Utf8JsonReader reader,
      Type typeToConvert,
      JsonSerializerOptions options) => reader.GetString()?.Trim() ?? string.Empty;
    
    public override void Write(
      Utf8JsonWriter writer,
      string value,
      JsonSerializerOptions options) => writer.WriteStringValue(value);
  }
}
