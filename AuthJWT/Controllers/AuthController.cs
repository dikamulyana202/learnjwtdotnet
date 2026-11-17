using AuthJWT.Entities;
using AuthJWT.Models;

using System.Security.Claims;
using System.Text;

using Microsoft.IdentityModel.Tokens;
// using Microsoft.IdentityModel.JsonWebTokens;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using AuthJWT.Services;
using Microsoft.AspNetCore.Authorization;

namespace AuthJWT.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class AuthController(IAuthService authService) : ControllerBase
   {
      [HttpPost("register")]
      public async Task<ActionResult<User>> Register(UserDto request)
      {
         var user = await authService.RegisterAsync(request);
         if (user is null)
         {
            return BadRequest("User Sudah Aya.");
         }

         return Ok(user);
      }

      [HttpPost("login")]

      public async Task<ActionResult<TokenResponseDto>> Login(UserDto request)
      {
         var result = await authService.LoginAsync(request);
         if (result is null)
         {
            return BadRequest("Username atawa Katasandina Salah.");
         }
         return Ok(result);
      }

      [HttpPost("refresh-token")]
      public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
      {
         var result = await authService.RefreshTokenAsync(request);
         if (result is null)
            return Unauthorized("refresh token na ewehan atau basi mang");

         return Ok(result);
      }

      [Authorize]
      [HttpGet]
      public IActionResult AuthenticatedOnlyEndpoint()
      {
         return Ok("u uda login bwang!");
      }

      [Authorize(Roles = "Admin, SuperAdmin")]
      [HttpGet("admin-only")]
      public IActionResult AdminOnlyEndpoint()
      {
         return Ok("sungkem admin!");
      }
   }
}
