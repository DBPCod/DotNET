namespace Backend.Dtos.Requests;

public class ForgotPasswordRequest
{
    public string Password { get; set; } = "";
    public string ConfirmPassword { get; set; } = "";
}