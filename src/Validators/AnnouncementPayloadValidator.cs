using FluentValidation;
using Prometheus.Models.Announcement;

namespace Prometheus.Validators; 

public class AnnouncementPayloadValidator : AbstractValidator<AnnouncementPayload> {
  public AnnouncementPayloadValidator() {
    RuleFor(x => x.Title).NotNull().MinimumLength(2);
    RuleFor(x => x.Content).NotNull().MinimumLength(2);
  }
}
