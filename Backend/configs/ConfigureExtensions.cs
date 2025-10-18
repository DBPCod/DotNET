using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Text.Json.Serialization;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Backend.Initializers;

namespace Backend.Configs;

public class ConfigureExtensions
{
    public static void ConfigureAllBuilder(WebApplicationBuilder builder)
    {
        ConfigureAddScoped(builder);
        ConfigureDevelopment(builder);
        ConfigureVariable(builder);
        ConfigureCors(builder);
        ConfigureControllers(builder);
        ConfigureAuthenticationJwt(builder);
        ConfigureAuthorizationPolicies(builder);
        ConfigureDbContext(builder);
        ConfigureRedis(builder);
        ConfigureHostedServices(builder);
    }

    private static void ConfigureAddScoped(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<MailHelper>();

        // Repositories
        builder.Services.AddScoped<UserRepository>();

        // Services
        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<AuthService>();
    }

    private static void ConfigureDevelopment(WebApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
        {
            builder.WebHost.UseUrls($"http://localhost:{Variable.Enviroments.PORT_HTTP}");
        }
    }

    private static void ConfigureVariable(WebApplicationBuilder builder)
    {
        var variable = new Variable();
        builder.Configuration.Bind(variable);
        builder.Services.AddSingleton(variable);
    }

    private static void ConfigureRedis(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<RedisHelper>();
        builder.Services.AddSingleton<RedisHosted>();
        builder.Services.AddHostedService(provider => provider.GetRequiredService<RedisHosted>());
    }

    private static void ConfigureHostedServices(WebApplicationBuilder builder)
    {
        builder.Services.AddHostedService<RootUserInitializer>();
    }

    private static void ConfigureDbContext(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(Variable.Enviroments.MYSQL_URI, ServerVersion.AutoDetect(Variable.Enviroments.MYSQL_URI))
        );
    }

    private static void ConfigureCors(WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(Variable.Constants.MyAllowSpecificOrigins, policy =>
            {
                // Allow both Next.js and Blazor frontend origins
                policy.WithOrigins(
                    Variable.Enviroments.CLIENT_URL,    // Next.js (http://localhost:3000)
                    "https://localhost:5001",           // Blazor HTTPS
                    "http://localhost:5000"             // Blazor HTTP
                ).AllowAnyMethod()
                 .AllowAnyHeader()
                 .AllowCredentials();

                // For development, log CORS configuration
                Console.WriteLine($"CORS configured for origins: {Variable.Enviroments.CLIENT_URL}, https://localhost:5001, http://localhost:5000");
            });
        });
    }

    private static void ConfigureControllers(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonNullEmptyIgnoreConverter());
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
    }

    private static void ConfigureAuthenticationJwt(WebApplicationBuilder builder)
    {
        var jwtKey = Encoding.UTF8.GetBytes(Variable.Enviroments.JWT_SECRET);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Variable.Enviroments.JWT_ISSUER,
                ValidAudience = Variable.Enviroments.JWT_AUDIENCE,
                IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
                ClockSkew = TimeSpan.Zero
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    // 1. Kiểm tra header Authorization: Bearer <token>
                    var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                    if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
                    {
                        context.Token = authHeader.Substring("Bearer ".Length).Trim();
                        return Task.CompletedTask;
                    }

                    // 2. Nếu không có header, fallback sang cookie
                    if (context.Request.Cookies.ContainsKey("access-token"))
                    {
                        context.Token = context.Request.Cookies["access-token"];
                    }

                    return Task.CompletedTask;
                },
                OnTokenValidated = async context =>
                {
                    var redisHelper = context.HttpContext.RequestServices.GetRequiredService<Utils.Helpers.RedisHelper>();
                    var token = context.SecurityToken as JwtSecurityToken;
                    if (token != null)
                    {
                        var blacklistKey = $"blacklist:{token.RawData}";
                        var isBlacklisted = await redisHelper.GetValue(blacklistKey);
                        if (!string.IsNullOrEmpty(isBlacklisted))
                        {
                            context.Fail("Token has been revoked");
                        }
                    }
                }
            };
        })
        .AddGoogle(googleOptions =>
        {
            googleOptions.ClientId = Variable.Enviroments.GOOGLE_CLIENT_ID;
            googleOptions.ClientSecret = Variable.Enviroments.GOOGLE_CLIENT_SECRET;
            googleOptions.CallbackPath = "/signin-google";
            googleOptions.SaveTokens = true;

            // Bạn có thể cấu hình thêm các scope nếu cần
            googleOptions.Scope.Add("email");
            googleOptions.Scope.Add("profile");

            // Xử lý event khi nhận thông tin từ Google
            googleOptions.Events = new OAuthEvents
            {
                OnCreatingTicket = context =>
                {
                    // Lưu thông tin bổ sung nếu cần
                    return Task.CompletedTask;
                }
            };
        });
    }

    private static void ConfigureAuthorizationPolicies(WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorizationBuilder()
            .AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"))
            .AddPolicy("UserOnly", policy => policy.RequireRole("User"));
    }
}
