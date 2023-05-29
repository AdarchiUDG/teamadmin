using FluentValidation;
using Prometheus.Models.Notifications;

namespace Prometheus.Validators; 

public class PushTokenPayloadValidator : AbstractValidator<PushTokenPayload> {
  public PushTokenPayloadValidator() {
    RuleFor(x => x.Token).NotNull().NotEmpty();
  }
}
