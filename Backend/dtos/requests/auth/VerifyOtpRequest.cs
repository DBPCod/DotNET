namespace Backend.Dtos.Requests;

public class VerifyOtpRequest
{
    public string Otp { get; set; } = "";
    public bool IsActivation { get; set; } = true;
}