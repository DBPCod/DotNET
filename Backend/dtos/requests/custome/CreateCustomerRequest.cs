using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos.Requests;

public class CreateCustomerRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = "";

    [MaxLength(20)]
    public string? Phone { get; set; }

    [MaxLength(100)]
    public string? Email { get; set; }

    public string? Address { get; set; }

    [MaxLength(20)]
    public string? Status { get; set; } = "ACTIVE"; // ACTIVE hoặc PENDING
}
