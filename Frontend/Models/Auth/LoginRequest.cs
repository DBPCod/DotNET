namespace Frontend.Models.Auth;

public class LoginRequest
{
    public string UsernameOrEmail { get; set; } = "";
    public string Password { get; set; } = "";
}
