using System.Text.Json;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Prometheus.ServiceExtensions; 

public static class SwaggerExtensions {
  private static readonly OpenApiSecurityScheme JwtSecurityScheme = new OpenApiSecurityScheme {
    BearerFormat = "JWT",
    Name = "JWT Authentication",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.Http,
    Scheme = JwtBearerDefaults.AuthenticationScheme,
    Description = "Put **_ONLY_** your JWT Bearer token on the textbox!",

    Reference = new OpenApiReference {
      Id = JwtBearerDefaults.AuthenticationScheme,
      Type = ReferenceType.SecurityScheme
    }
  };
  public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services) {
    services.AddEndpointsApiExplorer()
      .AddSwaggerGen(c => {
        c.EnableAnnotations();

        c.AddSecurityDefinition(JwtSecurityScheme.Reference.Id, JwtSecurityScheme);

        c.OperationFilter<AdditionalParametersOperationFilter>();
        c.OperationFilter<AuthorizationOperationFilter>();
      });

    services.AddFluentValidationRulesToSwagger();

    return services;
  }

  private class AdditionalParametersOperationFilter : IOperationFilter {  
    public void Apply(OpenApiOperation operation, OperationFilterContext context) {  
      operation.Parameters ??= new List<OpenApiParameter>();

      var splitTags = operation.Tags.Last().Name.Split(" ");

      var tag = "";
      if (splitTags.Length > 1) {
        tag = string.Join("", splitTags[..^1]);
      }
      
      operation.OperationId = JsonNamingPolicy.CamelCase.ConvertName($"{tag}{operation.Summary.Replace(" ", "")}");

      foreach (var value in context.ApiDescription.ActionDescriptor.EndpointMetadata) {
        if (value is not OpenApiParameter parameter) continue;

        operation.Parameters.Add(parameter);
      }
    }  
  }

  private class AuthorizationOperationFilter : IOperationFilter {
    public void Apply(OpenApiOperation operation, OperationFilterContext context) {  
      operation.Parameters ??= new List<OpenApiParameter>();

      var hasAllowAnonymous = context.ApiDescription.ActionDescriptor.EndpointMetadata.OfType<IAllowAnonymous>().Any();

      if (hasAllowAnonymous) return;
      
      operation.Security.Add(new OpenApiSecurityRequirement {
        { JwtSecurityScheme, Array.Empty<string>() }
      });

      operation.Responses.Add("401", new OpenApiResponse {
        Description = "Unauthorized"
      });
    }  
  }
}
