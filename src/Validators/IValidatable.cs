using FluentValidation.Results;

namespace Prometheus.Validators; 

public interface IValidatable {
  ValidationResult Validate();
}
