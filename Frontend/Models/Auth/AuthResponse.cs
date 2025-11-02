namespace Frontend.Models.Auth;

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

public class UserDto
{
    public string Id { get; set; } = "";
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public string FullName { get; set; } = "";
    public string Role { get; set; } = "";
    public string Status { get; set; } = "";
    public DateTime CreatedAt { get; set; }
}
