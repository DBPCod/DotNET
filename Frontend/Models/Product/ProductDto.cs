namespace Frontend.Models.Product;

public class ProductDto
{
    public string Id { get; set; } = "";
    public string? CategoryId { get; set; }
    public string? SupplierId { get; set; }
    public string ProductName { get; set; } = "";
    public string? Barcode { get; set; }
    public decimal Price { get; set; }
    public string Unit { get; set; } = "pcs";
    public string? ImagePath { get; set; }
    public bool Status { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    
    // Navigation properties (optional)
    public string? CategoryName { get; set; }
    public string? SupplierName { get; set; }
}