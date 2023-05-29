using FluentValidation;
using Prometheus.Models.Auth;

namespace Prometheus.Validators; 

public class AuthPayloadValidator : AbstractValidator<AuthPayload> {
  public AuthPayloadValidator() {
    RuleFor(x => x.Email).NotNull().EmailAddress();
    RuleFor(x => x.Password).NotNull().MinimumLength(8);
  }  
}
