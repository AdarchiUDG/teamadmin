using FluentValidation;
using Prometheus.Models.Payment;

namespace Prometheus.Validators; 

public class PaymentPayloadValidator : AbstractValidator<PaymentPayload> {
  public PaymentPayloadValidator() {
    RuleFor(x => x.Name).NotNull().MinimumLength(2);
    RuleFor(x => x.Amount).NotNull().GreaterThan(0);
  }
}
