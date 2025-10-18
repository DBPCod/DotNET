using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Services.Apis;

public class AuthService(UserService userService, RedisHelper redisHelper)
{
    private readonly UserService _userService = userService;
    private readonly RedisHelper _redisHelper = redisHelper;

    public string HandleGenerateAccessToken(UserDto userDto)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, userDto.Email),
            new Claim(ClaimTypes.Role, userDto.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Variable.Enviroments.JWT_SECRET));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: Variable.Enviroments.JWT_ISSUER,
            audience: Variable.Enviroments.JWT_AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                Variable.Enviroments.JWT_ACCESS_TOKEN_EXPIRY_MINUTES),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string HandleGenerateRefreshToken(UserDto userDto)
    {
        var email = userDto.Email ?? "unknown";

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Variable.Enviroments.JWT_SECRET));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(
                Variable.Enviroments.JWT_REFRESH_TOKEN_EXPIRY_DAYS),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal HandleGetPrincipalFromExpiredToken(string token)
    {
        var parameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Variable.Enviroments.JWT_SECRET)),
            ValidateLifetime = false
        };

        var handler = new JwtSecurityTokenHandler();
        var principal = handler.ValidateToken(token, parameters, out SecurityToken securityToken);

        if (!(securityToken is JwtSecurityToken jwtSecurityToken) ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }

    public async Task<Response> Register(RegisterRequest request)
    {
        var response = new Response();

        try
        {
            if (string.IsNullOrEmpty(request.Username))
                throw new ExceptionCustom(400, "Username is required");

            if (string.IsNullOrEmpty(request.Email))
                throw new ExceptionCustom(400, "Email is required");

            if (string.IsNullOrEmpty(request.Password))
                throw new ExceptionCustom(400, "Password is required");

            if (string.IsNullOrEmpty(request.ConfirmPassword))
                throw new ExceptionCustom(400, "Confirm Password is required");

            if (request.Password != request.ConfirmPassword)
                throw new ExceptionCustom(400, "Passwords do not match");

            var user = await _userService.HandleCreateUser(request.Username, request.Email, request.Password, "", "") ?? throw new ExceptionCustom(400, "Failed to create user");
            var userDto = UserMapper.MapEntityToDto(user);

            response.Message = "User registered successfully";
            response.StatusCode = 201;
            response.Data!.User = userDto;
        }
        catch (ExceptionCustom ex)
        {
            response.Message = ex.Message;
            response.StatusCode = ex.StatusCode;
            Console.WriteLine($">>> {ex.Message}");
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.StatusCode = 500;
            Console.WriteLine($">>> {ex.Message}");
        }

        return response;
    }

    public async Task<Response> Login(LoginRequest Request)
    {
        var response = new Response();

        try
        {
            if (string.IsNullOrEmpty(Request.UsernameOrEmail))
                throw new ExceptionCustom(400, "Username or Email is required");

            if (string.IsNullOrEmpty(Request.Password))
                throw new ExceptionCustom(400, "Password is required");

            var user = await _userService.HandleGetUserByUsernameOrEmail(Request.UsernameOrEmail) ?? throw new ExceptionCustom(404, "User not found");

            if (!BCrypt.Net.BCrypt.Verify(Request.Password, user.Password))
                throw new ExceptionCustom(403, "Invalid credentials");

            var userDto = UserMapper.MapEntityToDto(user);

            response.Message = "User logged in successfully";
            response.StatusCode = 200;
            response.Data!.User = userDto;
        }
        catch (ExceptionCustom ex)
        {
            response.Message = ex.Message;
            response.StatusCode = ex.StatusCode;
            Console.WriteLine($">>> {ex.Message}");
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.StatusCode = 500;
            Console.WriteLine($">>> {ex.Message}");
        }

        return response;
    }

    private async Task<string> HandleCreateAndStoreOtp(string email)
    {
        try
        {
            var rateLimitKey = $"otp:ratelimit:{email}";
            var existing = await _redisHelper.GetValue(rateLimitKey);

            if (!string.IsNullOrEmpty(existing))
            {
                throw new ExceptionCustom(429, "Too many requests");
            }

            var rnd = new Random();
            var otp = rnd.Next(100000, 999999).ToString();
            var otpKey = $"otp:{email}";

            // Lưu OTP trong 5 phút
            await _redisHelper.StoreValue(otpKey, otp, TimeSpan.FromMinutes(5));

            // Lưu rate limit trong 60 giây
            await _redisHelper.StoreValue(rateLimitKey, "1", TimeSpan.FromSeconds(60));

            return otp;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Redis error in HandleCreateAndStoreOtp: {ex.Message}");
            // Fallback when Redis is not available - return OTP but skip rate limiting
            var rnd = new Random();
            return rnd.Next(100000, 999999).ToString();
        }
    }

    public async Task<Response> SendOtp(string email)
    {
        var response = new Response();

        try
        {
            if (string.IsNullOrEmpty(email))
                throw new ExceptionCustom(400, "Email is required");

            var otp = await HandleCreateAndStoreOtp(email);

            await MailHelper.SendMail(email, "Your OTP Code", MailHelper.Template.SEND_OTP, new Dictionary<string, string>
            {
                { "EMAIL", email },
                { "OTP", otp },
                { "EXPIRE_MINUTES", "5" },
            });

            response.Message = "OTP sent successfully";
            response.StatusCode = 200;
        }
        catch (ExceptionCustom ex)
        {
            response.Message = ex.Message;
            response.StatusCode = ex.StatusCode;
            Console.WriteLine($">>> {ex.Message}");
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.StatusCode = 500;
            Console.WriteLine($">>> {ex.Message}");
        }

        return response;
    }

    public async Task<Response> VerifyOtp(string email, VerifyOtpRequest request)
    {
        var response = new Response();

        try
        {
            if (string.IsNullOrEmpty(email))
                throw new ExceptionCustom(400, "Email is required");

            if (string.IsNullOrEmpty(request.Otp))
                throw new ExceptionCustom(400, "OTP is required");

            var otpKey = $"otp:{email}";

            var storedOTP = await _redisHelper.GetValue(otpKey);

            if (string.IsNullOrEmpty(storedOTP))
                throw new ExceptionCustom(400, "Invalid OTP");

            response.Message = "OTP verified successfully";
            response.StatusCode = 200;
        }
        catch (ExceptionCustom ex)
        {
            response.Message = ex.Message;
            response.StatusCode = ex.StatusCode;
            Console.WriteLine($">>> {ex.Message}");
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.StatusCode = 500;
            Console.WriteLine($">>> {ex.Message}");
        }

        return response;
    }

    public async Task<Response> ForgotPassword(string email, ForgotPasswordRequest request)
    {
        var response = new Response();

        try
        {
            if (string.IsNullOrEmpty(email))
                throw new ExceptionCustom(400, "Email is required");

            if (string.IsNullOrEmpty(request.Password))
                throw new ExceptionCustom(400, "Password is required");

            if (string.IsNullOrEmpty(request.ConfirmPassword))
                throw new ExceptionCustom(400, "Confirm Password is required");

            if (request.Password != request.ConfirmPassword)
                throw new ExceptionCustom(400, "Passwords do not match");

            var user = await _userService.HandleGetUserByEmail(email) ?? throw new ExceptionCustom(404, "User not found");

            await _userService.HandleUpdateUserPassword(user, request.Password);

            response.Message = "Password changed successfully";
            response.StatusCode = 200;
        }
        catch (ExceptionCustom ex)
        {
            response.Message = ex.Message;
            response.StatusCode = ex.StatusCode;
            Console.WriteLine($">>> {ex.Message}");
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.StatusCode = 500;
            Console.WriteLine($">>> {ex.Message}");
        }

        return response;
    }

    public async Task<Response> ChangePassword(string email, ChangePasswordRequest request)
    {
        var response = new Response();

        try
        {
            if (string.IsNullOrEmpty(email))
                throw new ExceptionCustom(400, "Email is required");

            if (string.IsNullOrEmpty(request.OldPassword))
                throw new ExceptionCustom(400, "Old Password is required");

            if (string.IsNullOrEmpty(request.Password))
                throw new ExceptionCustom(400, "Password is required");

            if (string.IsNullOrEmpty(request.ConfirmPassword))
                throw new ExceptionCustom(400, "Confirm Password is required");

            if (request.Password != request.ConfirmPassword)
                throw new ExceptionCustom(400, "Passwords do not match");

            if (request.OldPassword == request.Password)
                throw new ExceptionCustom(400, "New password must be different from old password");

            var user = await _userService.HandleGetUserByEmail(email) ?? throw new ExceptionCustom(404, "User not found");

            if (!BCrypt.Net.BCrypt.Verify(request.OldPassword, user.Password))
                throw new ExceptionCustom(403, "Invalid credentials");

            await _userService.HandleUpdateUserPassword(user, request.Password);

            response.Message = "Password changed successfully";
            response.StatusCode = 200;
        }
        catch (ExceptionCustom ex)
        {
            response.Message = ex.Message;
            response.StatusCode = ex.StatusCode;
            Console.WriteLine($">>> {ex.Message}");
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.StatusCode = 500;
            Console.WriteLine($">>> {ex.Message}");
        }

        return response;
    }

    public async Task BlacklistToken(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var expiry = jwtToken.ValidTo;

            var timeToExpiry = expiry - DateTime.UtcNow;
            if (timeToExpiry > TimeSpan.Zero)
            {
                var blacklistKey = $"blacklist:{token}";
                await _redisHelper.StoreValue(blacklistKey, "blacklisted", timeToExpiry);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($">>> Error blacklisting token: {ex.Message}");
        }
    }
}