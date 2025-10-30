using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Backend.Dtos.Requests;

public class CreateProductRequest
{
    [Required(ErrorMessage = "Product name is required")]
    [MaxLength(100, ErrorMessage = "Product name cannot exceed 100 characters")]
    public string ProductName { get; set; } = "";

    [MaxLength(50, ErrorMessage = "Barcode cannot exceed 50 characters")]
    public string? Barcode { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }

    [MaxLength(20, ErrorMessage = "Unit cannot exceed 20 characters")]
    public string Unit { get; set; } = "pcs";

    public string? CategoryId { get; set; }

    public string? SupplierId { get; set; }

    // Upload hình ảnh sản phẩm
    public IFormFile? Image { get; set; }
}