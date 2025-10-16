namespace Backend.Configs;

public class Variable
{
    public static EnviromentVariables Enviroments { get; set; } = new EnviromentVariables();
    public static ConstantVariables Constants { get; set; } = new ConstantVariables();
}

public class EnviromentVariables
{
    // Server ports
    public int PORT_HTTP { get; } = int.Parse(Environment.GetEnvironmentVariable("PORT_HTTP") ?? "0");
    public int PORT_HTTPS { get; } = int.Parse(Environment.GetEnvironmentVariable("PORT_HTTPS") ?? "0");
    public int PORT_HTTP_SECONDARY { get; } = int.Parse(Environment.GetEnvironmentVariable("PORT_HTTP_SECONDARY") ?? "0");

    // Client URL
    public string CLIENT_URL { get; } = Environment.GetEnvironmentVariable("CLIENT_URL") ?? "";

    // JWT settings
    public string JWT_SECRET { get; } = Environment.GetEnvironmentVariable("JWT_SECRET") ?? "";
    public string JWT_ISSUER { get; } = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "";
    public string JWT_AUDIENCE { get; } = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "";
    public int JWT_ACCESS_TOKEN_EXPIRY_MINUTES { get; } = int.Parse(Environment.GetEnvironmentVariable("JWT_ACCESS_TOKEN_EXPIRY_MINUTES") ?? "0");
    public int JWT_REFRESH_TOKEN_EXPIRY_DAYS { get; } = int.Parse(Environment.GetEnvironmentVariable("JWT_REFRESH_TOKEN_EXPIRY_DAYS") ?? "0");

    // MySQL
    public string MYSQL_URI { get; } = Environment.GetEnvironmentVariable("MYSQL_URI") ?? "";

    // Redis
    public string REDIS_HOST { get; } = Environment.GetEnvironmentVariable("REDIS_HOST") ?? "";
    public int REDIS_PORT { get; } = int.Parse(Environment.GetEnvironmentVariable("REDIS_PORT") ?? "0");
    public string REDIS_USERNAME { get; } = Environment.GetEnvironmentVariable("REDIS_USERNAME") ?? "";
    public string REDIS_PASSWORD { get; } = Environment.GetEnvironmentVariable("REDIS_PASSWORD") ?? "";

    // Google OAuth
    public string GOOGLE_CLIENT_ID { get; } = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID") ?? "";
    public string GOOGLE_CLIENT_SECRET { get; } = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET") ?? "";

    // Mail
    public string SMTP_HOST { get; } = Environment.GetEnvironmentVariable("SMTP_HOST") ?? "";
    public string SMTP_PORT { get; } = Environment.GetEnvironmentVariable("SMTP_PORT") ?? "";
    public string SMTP_USER { get; } = Environment.GetEnvironmentVariable("SMTP_USER") ?? "";
    public string SMTP_PASS { get; } = Environment.GetEnvironmentVariable("SMTP_PASS") ?? "";
    public string SMTP_FROM { get; } = Environment.GetEnvironmentVariable("SMTP_FROM") ?? "";

    // Mail
    public string ROOT_FULLNAME { get; } = Environment.GetEnvironmentVariable("ROOT_FULLNAME") ?? "";
    public string ROOT_EMAIL { get; } = Environment.GetEnvironmentVariable("ROOT_EMAIL") ?? "";
    public string ROOT_PASSWORD { get; } = Environment.GetEnvironmentVariable("ROOT_PASSWORD") ?? "";
}

public class ConstantVariables
{
    public string MyAllowSpecificOrigins { get; } = "_myAllowSpecificOrigins";
}
