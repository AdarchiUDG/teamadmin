using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Prometheus.Data;
using Prometheus.Data.Entities;

namespace Prometheus.ServiceExtensions;

public static class AuthenticationExtensions {
  public static IServiceCollection AddAuthenticationEngine(this IServiceCollection services) {
    services.AddAuthenticationCore()
      .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters {
          ValidateIssuer = false,
          ValidateAudience = false,
          ValidateIssuerSigningKey = true,
          ValidateLifetime = true,
          ValidIssuer = Environment.GetEnvironmentVariable("BASE_URL"),
          ValidAudience = Environment.GetEnvironmentVariable("BASE_URL"),
          IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY")!))
        };

        options.Events = new JwtBearerEvents {
          OnTokenValidated = async context => {
            var database = context.HttpContext.RequestServices.GetRequiredService<DatabaseContext>();
            
            var userId = context.Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (context.Principal is null || userId is null) {
              context.Fail("Invalid token");
              return;
            }

            var id = Guid.Parse(userId);
            var user = await database.Users
              .Include(u => u.Roles)
              .Select(u => new { u.Id, u.Deleted, u.Roles }).FirstOrDefaultAsync(u => !u.Deleted && u.Id == id);

            if (user is null) {
              context.Fail("User does not exist or has been removed");
              return;
            }

            foreach (var identity in context.Principal.Identities) {
              var claims = identity.FindAll(ClaimTypes.Role).ToArray();

              foreach (var claim in claims) {
                identity.TryRemoveClaim(claim);
              }
              identity.AddClaims(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Slug)));
            }

            context.HttpContext.Items.Add("currentUserId", id);
          }
        };
      });

    services.AddAuthorization(options => {
      options.AddPolicy("admin", policy => {
        policy.RequireRole("administrator");
      });
      options.AddPolicy("teacher", policy => {
        policy.RequireRole("teacher", "administrator");
      });
      options.AddPolicy("general", policy => {
        policy.RequireAuthenticatedUser();
      });
      options.DefaultPolicy = options.GetPolicy("admin")!;
    });

    return services;
  }
}
