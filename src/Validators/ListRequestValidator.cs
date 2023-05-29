using FluentValidation;
using Prometheus.Endpoints;
using Prometheus.Models.Requests;

namespace Prometheus.Validators;

public class ListRequestValidator : AbstractValidator<ListRequest> {
  public ListRequestValidator() {
    RuleFor(x => x.Limit).InclusiveBetween(1, 1000);
    RuleFor(x => x.Page).GreaterThan(0);
  }
}