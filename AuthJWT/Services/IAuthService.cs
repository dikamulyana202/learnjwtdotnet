using AuthJWT.Entities;
using AuthJWT.Models;

namespace AuthJWT.Services
{
  public interface IAuthService
  {
    Task<User?> RegisterAsync(UserDto request);
    Task<TokenResponseDto?> LoginAsync(UserDto request);
    Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request);
  }
}