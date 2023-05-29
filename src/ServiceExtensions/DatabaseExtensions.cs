using Microsoft.EntityFrameworkCore;
using Prometheus.Data;
using Prometheus.Models;

namespace Prometheus.ServiceExtensions; 

public static class DatabaseExtensions {
  public static IServiceCollection AddDatabase(this IServiceCollection services) {
    services.AddDbContextPool<DatabaseContext>(builder => {
        builder.UseNpgsql(Environment.GetEnvironmentVariable("CONN_STRING"));
      }).AddAutoMapper(c => {
        c.AddProfile<ChildrenTeamsProfile>();
      });

    services.AddMemoryCache();
    
    return services;
  }
}
