namespace Frontend.Models.Auth;

public class ForgotPasswordRequest
{
    public string Password { get; set; } = "";
    public string ConfirmPassword { get; set; } = "";
}
