using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Backend.Dtos.Requests;

public class UpdateProductRequest
{
    [Required(ErrorMessage = "Product name is required")]
    [MaxLength(100, ErrorMessage = "Product name cannot exceed 100 characters")]
    public string ProductName { get; set; } = "";

    [MaxLength(50, ErrorMessage = "Barcode cannot exceed 50 characters")]
    public string? Barcode { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Unit is required")]
    [MaxLength(20, ErrorMessage = "Unit cannot exceed 20 characters")]
    public string Unit { get; set; } = "pcs";

    // ✅ Cho phép null để có thể xóa category/supplier
    public string? CategoryId { get; set; }

    public string? SupplierId { get; set; }

    // ✅ Status là required và mặc định true
    [Required(ErrorMessage = "Status is required")]
    public bool Status { get; set; } = true;

    // Upload hình ảnh sản phẩm (tùy chọn khi update)
    public IFormFile? Image { get; set; }
}