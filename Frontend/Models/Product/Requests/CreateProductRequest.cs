using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;

namespace Frontend.Models.Product.Requests;

/// <summary>
/// Request để tạo product mới
/// </summary>
public class CreateProductRequest
{
    [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
    [StringLength(100, ErrorMessage = "Tên sản phẩm không được vượt quá 100 ký tự")]
    public string ProductName { get; set; } = "";

    [StringLength(50, ErrorMessage = "Barcode không được vượt quá 50 ký tự")]
    public string? Barcode { get; set; }

    [Required(ErrorMessage = "Giá là bắt buộc")]
    [Range(0.01, 999999999.99, ErrorMessage = "Giá phải lớn hơn 0")]
    public decimal Price { get; set; }

    [StringLength(20, ErrorMessage = "Đơn vị không được vượt quá 20 ký tự")]
    public string Unit { get; set; } = "pcs";

    public string? CategoryId { get; set; }
    public string? SupplierId { get; set; }
    public bool Status { get; set; } = true;
    
    // Note: Image upload sẽ được xử lý riêng
    public IBrowserFile? Image { get; set; }
}

