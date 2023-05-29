using FluentValidation;
using Prometheus.Models.Auth;

namespace Prometheus.Validators; 

public class ChangePasswordPayloadValidator : AbstractValidator<ChangePasswordPayload> {
  public ChangePasswordPayloadValidator() {
    RuleFor(x => x.NewPassword).NotNull().MinimumLength(8);
    RuleFor(x => x.OldPassword).NotNull().MinimumLength(8);
  }  
}
