namespace Backend.Dtos.Responses;

public class ProductDto
{
    public string? Id { get; set; }
    public string? CategoryId { get; set; }
    public string? SupplierId { get; set; }
    public string? ProductName { get; set; }
    public string? Barcode { get; set; }
    public decimal Price { get; set; }
    public string? Unit { get; set; }
    public string? ImagePath { get; set; } // Đường dẫn ảnh
    public bool Status { get; set; } // true = Active, false = Deleted
    public DateTime CreatedAt { get; set; }
}