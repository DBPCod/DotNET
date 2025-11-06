using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos.Requests;

public class CreateSupplierRequest
{
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; } = "";

    [MaxLength(20, ErrorMessage = "Phone cannot exceed 20 characters")]
    public string? Phone { get; set; }

    [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string? Email { get; set; }

    public string? Address { get; set; }

    public bool Status { get; set; } = true;
}