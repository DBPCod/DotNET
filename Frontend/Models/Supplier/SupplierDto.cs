namespace Frontend.Models.Supplier;

public class SupplierDto
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";  // ✅ Backend dùng "Name"
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public bool Status { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    
    // Helper property để hiển thị (optional)
    public string SupplierName => Name;  // Alias cho compatibility
}