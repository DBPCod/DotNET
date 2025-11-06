namespace Backend.Dtos;

public class CustomerDto
{
    public string Id { get; set; } = "";
    public string? CustomerId { get; set; } // CUS001, CUS002, ...
    public string Name { get; set; } = "";
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string Status { get; set; } = "ACTIVE"; // ACTIVE hoặc PENDING
    public DateTime CreatedAt { get; set; }
}