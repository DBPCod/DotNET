namespace Frontend.Models.User;

/// <summary>
/// User Data Transfer Object - dùng chung cho tất cả modules
/// </summary>
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