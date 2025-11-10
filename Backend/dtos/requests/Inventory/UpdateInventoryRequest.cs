using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos.Requests;

public class UpdateInventoryRequest
{
    [Required]
    public string ProductId { get; set; } = "";

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative number")]
    public int Quantity { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Cost price must be a non-negative number")]
    public decimal CostPrice { get; set; } = 0; // Giá nhập
}
