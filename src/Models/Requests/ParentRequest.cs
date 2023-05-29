using Microsoft.AspNetCore.Mvc;

namespace Prometheus.Models.Requests; 

public abstract class ParentRequest<TParentKey> : Request {
  [FromRoute]
  public TParentKey ParentId { get; set; }
}
