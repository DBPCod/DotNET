using System.ComponentModel.DataAnnotations;

namespace Frontend.Models.Inventory.Requests;

public class CreateInventoryRequest
{
    [Required(ErrorMessage = "Sản phẩm là bắt buộc")]
    public string ProductId { get; set; } = "";

    [Required(ErrorMessage = "Số lượng là bắt buộc")]
    [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải là số không âm")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Giá nhập là bắt buộc")]
    [Range(0, double.MaxValue, ErrorMessage = "Giá nhập phải là số không âm")]
    public decimal CostPrice { get; set; } = 0;
}

public class UpdateInventoryRequest
{
    [Required(ErrorMessage = "Sản phẩm là bắt buộc")]
    public string ProductId { get; set; } = "";

    [Required(ErrorMessage = "Số lượng là bắt buộc")]
    [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải là số không âm")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Giá nhập là bắt buộc")]
    [Range(0, double.MaxValue, ErrorMessage = "Giá nhập phải là số không âm")]
    public decimal CostPrice { get; set; } = 0;
}

public class AdjustInventoryRequest
{
    [Required(ErrorMessage = "Sản phẩm là bắt buộc")]
    public string ProductId { get; set; } = "";

    [Required(ErrorMessage = "Số lượng thay đổi là bắt buộc")]
    public int AdjustmentQuantity { get; set; } // có thể âm (xuất kho) hoặc dương (nhập kho)
    
    public string? Reason { get; set; } = ""; // Lý do điều chỉnh
}