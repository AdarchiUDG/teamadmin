using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Prometheus.Models.User;

namespace Prometheus.Validators; 

public class UserPayloadValidator : AbstractValidator<UserPayload> {
  public UserPayloadValidator() {
    RuleFor(x => x.Name)
      .NotNull().MinimumLength(2);
    RuleFor(x => x.LastName)
      .NotNull().MinimumLength(2);
    RuleFor(x => x.Email)
      .NotNull().EmailAddress();
    RuleFor(x => x.Phone)
      .NotNull().Must(value => new PhoneAttribute().IsValid(value));
    RuleFor(x => x.Roles).NotNull().NotEmpty();
    RuleForEach(x => x.Roles)
      .NotNull().GreaterThan(0).LessThanOrEqualTo(3);
  }
}
