using FluentValidation;
using Prometheus.Models.Team;

namespace Prometheus.Validators; 

public class TeamPayloadValidator : AbstractValidator<TeamPayload> {
  public TeamPayloadValidator() {
    RuleFor(x => x.Name).NotNull().MinimumLength(2);
    RuleFor(x => x.TrainerId).NotNull();
    RuleFor(x => x.Members).NotNull().NotEmpty();
    RuleForEach(x => x.Members).GreaterThan(0);
  }
}
