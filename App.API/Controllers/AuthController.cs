
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using App.API.Models.Requests;
using App.Services;
using App.API;

namespace App.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService tokenService;
        private readonly IUserService userService;
        public AuthController(TokenService tokenService, IUserService userService)
        {
            this.tokenService = tokenService;
            this.userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest loginRequest)
        {
            var user = await userService.LoginAsync(loginRequest.Email, loginRequest.Password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Username", user.UserName),
                    new Claim("Email", user.Email),
                    new Claim("Role", user.Role ?? "user")
                };

                string accessToken = tokenService.GenerateAccessToken(claims);
                string refreshToken = tokenService.GenerateRefreshToken();

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(7)
                };

                Response.Cookies.Append("RefreshToken", refreshToken, cookieOptions);

                return Ok(new { accessToken });
            }

            return Unauthorized();
        }
    }
}