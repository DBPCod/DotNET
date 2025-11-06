namespace Frontend.Models.Auth;

using Frontend.Models.User;

public class AuthResponse<T>
{
    public string Message { get; set; } = "";
    public int StatusCode { get; set; }
    public T? Data { get; set; }
}

public class LoginData
{
    public UserDto? User { get; set; }
}
