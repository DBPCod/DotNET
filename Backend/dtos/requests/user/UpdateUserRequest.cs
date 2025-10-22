using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos.Requests.User;

public class UpdateUserRequest
{
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
    public string? Username { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email format")]
    [StringLength(50, ErrorMessage = "Email cannot exceed 50 characters")]
    public string? Email { get; set; }

    [StringLength(255, ErrorMessage = "Full name cannot exceed 255 characters")]
    public string? FullName { get; set; }

    [RegularExpression("^(ADMIN|STAFF)$", ErrorMessage = "Role must be either ADMIN or STAFF")]
    public string? Role { get; set; }
}