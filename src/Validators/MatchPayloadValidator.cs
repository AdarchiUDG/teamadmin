using FluentValidation;
using Prometheus.Models.Match;

namespace Prometheus.Validators; 

public class MatchPayloadValidator : AbstractValidator<MatchPayload> {
  public MatchPayloadValidator() {
    RuleFor(x => x.StartDate).NotNull();
    RuleFor(x => x.FirstTeamId).NotNull().GreaterThan(0);
    RuleFor(x => x.SecondTeamId).NotNull().GreaterThan(0);
  }
}
