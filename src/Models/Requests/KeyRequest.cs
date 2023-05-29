using Microsoft.AspNetCore.Mvc;

namespace Prometheus.Models.Requests; 

public class KeyRequest<TKey> : Request {
  [FromRoute]
  public required TKey Id { get; set; }
}
