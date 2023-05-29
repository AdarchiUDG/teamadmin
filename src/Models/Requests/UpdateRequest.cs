using Microsoft.AspNetCore.Mvc;

namespace Prometheus.Models.Requests; 

public class UpdateRequest<TKey, TBody> : BodyRequest<TBody> {
  [FromRoute]
  public required TKey Id { get; set; }
}
