namespace AuthJWT.Entities;

public class User
{
  public Guid id { get; set; }
  public string Username { get; set; } = string.Empty;
  public string PasswordHash { get; set; } = string.Empty;
}
