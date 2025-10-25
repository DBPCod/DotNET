using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos.Requests.Promotion;

public class ValidatePromotionRequest
{
    [Required(ErrorMessage = "Promo code is required")]
    public string Code { get; set; } = "";

    [Required(ErrorMessage = "Order total is required")]
    [Range(0.01, 99999999.99, ErrorMessage = "Order total must be greater than 0")]
    public decimal OrderTotal { get; set; }
}

