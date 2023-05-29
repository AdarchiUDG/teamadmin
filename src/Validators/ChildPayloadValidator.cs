using FluentValidation;
using Prometheus.Data.Entities;
using Prometheus.Models.Child;

namespace Prometheus.Validators; 

public class ChildPayloadValidator : AbstractValidator<ChildPayload> {
  public ChildPayloadValidator() {
    RuleFor(x => x.Name).NotNull().MinimumLength(2);
    RuleFor(x => x.LastName).NotNull().MinimumLength(2);
    RuleFor(x => x.BirthDate).NotNull().LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now));
;  }
}
