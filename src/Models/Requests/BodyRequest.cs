using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Prometheus.Validators;

namespace Prometheus.Models.Requests; 

public class BodyRequest<TBody> : Request, IValidatable {
  [FromBody]
  public required TBody Payload { get; set; }
  
  [FromServices]
  public required IValidator<TBody> Validator { get; set; }

  public ValidationResult Validate() => Validator.Validate(Payload);
}
