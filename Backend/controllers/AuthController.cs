using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/v1/auth")]
[ApiController]
public class AuthController(AuthService authService, UserService userService) : ControllerBase
{
    private readonly AuthService _authService = authService;
    private readonly UserService _userService = userService;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegisterRequest request)
    {
        var response = await _authService.Register(request);

        return StatusCode(response.StatusCode, response.Message);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] LoginRequest request)
    {
        var response = await _authService.Login(request);

        if (response.Data.User != null)
        {
            var accessToken = _authService.HandleGenerateAccessToken(response.Data.User);
            var refreshToken = _authService.HandleGenerateRefreshToken(response.Data.User);

            Response.Cookies.Append("access-token", accessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                // SameSite = SameSiteMode.Strict,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddMinutes(
                    Variable.Enviroments.JWT_ACCESS_TOKEN_EXPIRY_MINUTES
                )
            });

            Response.Cookies.Append("refresh-token", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                // SameSite = SameSiteMode.Strict,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddDays(
                    Variable.Enviroments.JWT_REFRESH_TOKEN_EXPIRY_DAYS
                )
            });
        }

        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("logout")]
    [Authorize()]
    public async Task<IActionResult> Logout()
    {
        var accessToken = Request.Cookies["access-token"];
        var refreshToken = Request.Cookies["refresh-token"];

        if (!string.IsNullOrEmpty(accessToken))
        {
            await _authService.BlacklistToken(accessToken);
        }

        if (!string.IsNullOrEmpty(refreshToken))
        {
            await _authService.BlacklistToken(refreshToken);
        }

        Response.Cookies.Delete("access-token");
        Response.Cookies.Delete("refresh-token");

        return Ok(new { Message = "Logged out" });
    }

    [HttpPost("send-otp/{email}")]
    [Authorize()]
    public async Task<IActionResult> SendOtp(string email)
    {
        var response = await _authService.SendOtp(email);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("verify-otp/{email}")]
    [Authorize()]
    public async Task<IActionResult> VerifyOtp(string email, [FromForm] VerifyOtpRequest request)
    {
        var response = await _authService.VerifyOtp(email, request);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("forgot-password/{email}")]
    [Authorize()]
    public async Task<IActionResult> ForgotPassword(string email, [FromForm] ForgotPasswordRequest request)
    {
        var response = await _authService.ForgotPassword(email, request);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("change-password/{email}")]
    [Authorize()]
    public async Task<IActionResult> ChangePassword(string email, [FromForm] ChangePasswordRequest request)
    {
        var response = await _authService.ChangePassword(email, request);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> Refresh()
    {
        try
        {
            var refreshToken = Request.Cookies["refresh-token"];
            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized("Refresh token is missing");

            var principal = _authService.HandleGetPrincipalFromExpiredToken(refreshToken);

            var Email = principal.Identity?.Name;
            if (string.IsNullOrEmpty(Email))
                return Unauthorized("Invalid token claims");

            var user = await _userService.HandleGetUserByEmail(Email);
            if (user == null)
                return Unauthorized("User not found");

            var userDto = UserMapper.MapEntityToDto(user);
            var newAccessToken = _authService.HandleGenerateAccessToken(userDto);

            Response.Cookies.Append("access-token", newAccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(
                    int.Parse(Environment.GetEnvironmentVariable("JWT_ACCESS_TOKEN_EXPIRY_MINUTES")!)
                )
            });

            return NoContent();
        }
        catch
        {
            return Unauthorized("Invalid refresh token");
        }
    }

    [HttpGet("health")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> HealthCheck()
    {
        var response = new Response()
        {
            Message = "Auth service is running",
            StatusCode = 200
        };
        return StatusCode(response.StatusCode, response);
    }
}