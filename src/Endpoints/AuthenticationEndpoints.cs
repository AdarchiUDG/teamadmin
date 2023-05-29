using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Prometheus.Data.Entities;
using Prometheus.Models.Auth;
using Prometheus.Models.Requests;
using Prometheus.Models.User;
using Prometheus.Security;

namespace Prometheus.Endpoints; 

public static class AuthenticationEndpoints {
  public static RouteGroupBuilder AddAuthenticationEndpoints(this RouteGroupBuilder builder) {
    builder.MapPost("login", Login)
      .Produces<AuthResponse>()
      .ProducesProblem(StatusCodes.Status401Unauthorized)
      .ProducesProblem(StatusCodes.Status500InternalServerError)
      .AllowAnonymous()
      .WithSwagger("Login", "Retrieves a token given valid credentials");
    
    builder.MapPost("change_password", ChangePassword)
      .Produces(StatusCodes.Status204NoContent)
      .ProducesProblem(StatusCodes.Status500InternalServerError)
      .RequireAuthorization("general")
      .WithSwagger("Change Password", "Changes the password of the current user");
    
    builder.MapPost("forgot_password", RequestResetPassword)
      .Produces(StatusCodes.Status204NoContent)
      .ProducesProblem(StatusCodes.Status404NotFound)
      .ProducesProblem(StatusCodes.Status500InternalServerError)
      .AllowAnonymous()
      .WithSwagger("Forgot Password", "Sends an email with information regarding a password update for the current user");
    
    builder.MapPost("reset_password", ResetPassword)
      .Produces(StatusCodes.Status204NoContent)
      .ProducesProblem(StatusCodes.Status404NotFound)
      .ProducesProblem(StatusCodes.Status500InternalServerError)
      .AllowAnonymous()
      .WithSwagger("Reset Password", "Resets the user's password");
    return builder;
  }

  private static async Task<IResult> Login([AsParameters] BodyRequest<AuthPayload> request) {
    var user = await request.Database.Users
      .Include(u => u.Roles)
      .FirstOrDefaultAsync(u => EF.Functions.ILike(u.Email, request.Payload.Email));
    
    if (user is null || !Crypto.VerifyPasswords(request.Payload.Password, user.Password)) {
      return Results.Unauthorized();
    }
    
    var key = Environment.GetEnvironmentVariable("SECRET_KEY");
    var issuer = Environment.GetEnvironmentVariable("BASE_URL");
    var audience = Environment.GetEnvironmentVariable("BASE_URL");

    if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience)) {
      return Results.StatusCode(StatusCodes.Status500InternalServerError);
    }

    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
    var claims = new List<Claim> {
      new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
      new (ClaimTypes.NameIdentifier, user.Id.ToString()),
      new (ClaimTypes.Email, user.Email),
    };
    claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Slug)));

    var expirationDate = DateTime.Now.AddDays(30);
    var token = new JwtSecurityToken(issuer, audience, claims, expires: expirationDate, signingCredentials: credentials);
    var jwt = new JwtSecurityTokenHandler().WriteToken(token);

    var cookieOptions = new CookieOptions() { HttpOnly = true };

    var refreshToken = Crypto.HashPassword(Guid.NewGuid().ToString());
    user.RefreshTokens.Add(new RefreshToken() {
      Token = refreshToken
    });

    await request.Database.SaveChangesAsync();
    
    return Results.Ok(new AuthResponse {
      Token = jwt,
      ExpiresAt = expirationDate,
      User = await request.Database.Users
        .ProjectTo<UserResponse>(request.MapperConfig)
        .FirstAsync(u => u.Id == user.Id),
    });
  }

  private static async Task<IResult> ChangePassword([AsParameters] BodyRequest<ChangePasswordPayload> request) {
    var userId = request.GetValue<Guid>("currentUserId");
    
    var user = await request.Database.Users
      .FirstOrDefaultAsync(u => u.Id == userId);
    
    if (user is null || !Crypto.VerifyPasswords(request.Payload.OldPassword, user.Password)) {
      return Results.Unauthorized();
    }

    user.Password = Crypto.HashPassword(request.Payload.NewPassword);
    await request.Database.SaveChangesAsync();

    return Results.NoContent();
  }

  private static async Task<IResult> RequestResetPassword([AsParameters] BodyRequest<RequestResetPasswordPayload> request) {
    var user = await request.Database.Users
      .FirstOrDefaultAsync(u => EF.Functions.ILike(u.Email, request.Payload.Email));

    if (user is null) {
      return Results.NotFound();
    }

    var code = Crypto.GeneratePassword(32);

    await request.Database.AddAsync(new PasswordRecovery() {
      Code = code,
      ExpiresAt = DateTime.UtcNow.Add(TimeSpan.FromMinutes(15)),
      User = user
    });

    await request.Database.SaveChangesAsync();
    
    await request.Mail.SendRecoveryEmail(user, code);

    return Results.NoContent();
  }

  private static async Task<IResult> ResetPassword([AsParameters] BodyRequest<ResetPasswordPayload> request) {
    var token = await request.Database.PasswordRecoveries
      .Include(t => t.User)
      .FirstOrDefaultAsync(p => p.ExpiresAt >= DateTime.UtcNow && EF.Functions.ILike(p.User.Email, request.Payload.Email) && p.Code == request.Payload.Code);

    if (token is null) {
      return Results.NotFound();
    }

    token.User.Password = Crypto.HashPassword(request.Payload.Password);
    await request.Database.SaveChangesAsync();

    return Results.NoContent();
  }
}
