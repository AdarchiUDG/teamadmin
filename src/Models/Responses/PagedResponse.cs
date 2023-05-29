using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace Prometheus.Models.Responses; 

public class PagedResponse<TValue> {
  public PagedResponse() { }

  public required IEnumerable<TValue> Results { get; set; }
  public required int Page { get; set; }
  public required int PageSize { get; set; }
  public required int Total { get; set; }
  public required int TotalPages { get; set; }
}
