namespace Backend.Dtos.Requests;

public class ChangePasswordRequest
{
    public string Password { get; set; } = "";
    public string OldPassword { get; set; } = "";
    public string ConfirmPassword { get; set; } = "";
}