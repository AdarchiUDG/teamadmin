using FluentValidation;
using Prometheus.Models.Auth;

namespace Prometheus.Validators; 

public class RequestResetPasswordPayloadValidator : AbstractValidator<RequestResetPasswordPayload> {
  public RequestResetPasswordPayloadValidator() {
    RuleFor(x => x.Email).EmailAddress().NotNull();
  }  
}
