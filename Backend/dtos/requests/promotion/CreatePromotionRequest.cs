using System.ComponentModel.DataAnnotations;
using Backend.Models;

namespace Backend.Dtos.Requests.Promotion;

public class CreatePromotionRequest
{
    [Required(ErrorMessage = "Promo code is required")]
    [StringLength(50, ErrorMessage = "Promo code cannot exceed 50 characters")]
    public string PromoCode { get; set; } = "";

    [StringLength(255, ErrorMessage = "Description cannot exceed 255 characters")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Discount type is required")]
    public DiscountType DiscountType { get; set; } = DiscountType.Percent;

    [Range(0, 99999999.99, ErrorMessage = "Discount value must be between 0 and 99999999.99")]
    public decimal DiscountValue { get; set; } = 0;

    [Required(ErrorMessage = "Promotion type is required")]
    public PromotionType PromotionType { get; set; } = PromotionType.Promotion;

    [Required(ErrorMessage = "Start date is required")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "End date is required")]
    public DateTime EndDate { get; set; }

    [Range(0, 99999999.99, ErrorMessage = "Minimum order amount must be between 0 and 99999999.99")]
    public decimal MinOrderAmount { get; set; } = 0;

    [Range(0, 999999, ErrorMessage = "Usage limit must be between 0 and 999999")]
    public int UsageLimit { get; set; } = 0;

    [Required(ErrorMessage = "Status is required")]
    public PromotionStatus Status { get; set; } = PromotionStatus.Active;
}
