namespace Prometheus.Security; 

public class Crypto {
  private const int WorkFactor = 13; 
  private const string CharacterPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
  public static string HashPassword(string input) =>
    BCrypt.Net.BCrypt.HashPassword(input, WorkFactor);

  public static bool VerifyPasswords(string text, string hash) =>
    BCrypt.Net.BCrypt.Verify(text, hash);

  public static string GeneratePassword(int length) {
    var random = new Random();
    var buffer = new byte[length];
    random.NextBytes(buffer);
    return new string(buffer.Select(b => CharacterPool[b % CharacterPool.Length]).ToArray());
  }
}
