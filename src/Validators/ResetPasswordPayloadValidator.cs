using FluentValidation;
using Prometheus.Models.Auth;

namespace Prometheus.Validators; 

public class ResetPasswordPayloadValidator : AbstractValidator<ResetPasswordPayload> {
  public ResetPasswordPayloadValidator() {
    RuleFor(x => x.Email).EmailAddress().NotNull();
    RuleFor(x => x.Code).Length(32).NotNull();
    RuleFor(x => x.Password).NotNull().MinimumLength(8);
  }  
}
