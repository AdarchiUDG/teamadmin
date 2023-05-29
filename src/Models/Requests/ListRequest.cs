using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Prometheus.Validators;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Prometheus.Models.Requests; 

public class ListRequest : Request, IValidatable {
  public required IValidator<ListRequest> Validator { get; set; }
  [FromQuery(Name = "limit"), Range(1, 1000), DefaultValue(25)]
  public required int Limit { get; set; } = 25;
  [FromQuery(Name = "page"), Range(1, int.MaxValue), DefaultValue(1)]
  public required int Page { get; set; } = 1;
  public int Offset => (Page - 1) * Limit;
  
  public ValidationResult Validate() {
    return Validator.Validate(this);
  }
}
