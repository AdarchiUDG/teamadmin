using FluentValidation;
using FluentValidation.AspNetCore;
using Prometheus.Validators;

namespace Prometheus.ServiceExtensions; 

public static class ValidationExtensions {
  public static IServiceCollection AddValidators(this IServiceCollection services) {
    services.AddFluentValidationClientsideAdapters();
    services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Singleton);
    return services;
  }
}
