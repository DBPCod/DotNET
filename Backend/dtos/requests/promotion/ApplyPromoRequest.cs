using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos.Requests.Promotion;

public class ApplyPromoRequest
{
    [Required(ErrorMessage = "Promo code is required")]
    public string Code { get; set; } = "";
}

