using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos.Requests.User;

public class CreateUserRequest 
{
    [Required(ErrorMessage = "Username is required")]
    [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters")]
    public string Username { get; set; } = "";

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [StringLength(50, ErrorMessage = "Email cannot exceed 50 characters")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Password is required")]
    [StringLength(255, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 255 characters")]
    public string Password { get; set; } = "";

    [StringLength(255, ErrorMessage = "Full name cannot exceed 255 characters")]
    public string FullName { get; set; } = "";

    [Required(ErrorMessage = "Role is required")]
    [RegularExpression("^(ADMIN|STAFF|USER)$", ErrorMessage = "Role must be ADMIN, STAFF or USER")]
    public string Role { get; set; } = "USER";

    [RegularExpression("^(ACTIVE|INACTIVE)$", ErrorMessage = "Status must be ACTIVE or INACTIVE")]
    public string Status { get; set; } = "ACTIVE";
}