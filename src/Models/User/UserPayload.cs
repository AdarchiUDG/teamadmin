using System.ComponentModel.DataAnnotations;

namespace Prometheus.Models.User;

public class UserPayload {
  public required string Name { get; init; }
  public required string LastName { get; init; }
  public required string Email { get; init; }
  [Phone]
  public required string Phone { get; init; }
  public required int[] Roles { get; init; }
}
