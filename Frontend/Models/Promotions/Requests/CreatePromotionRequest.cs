namespace Frontend.Models.Promotions.Requests;

public class CreatePromotionRequest
{
    public string PromoCode { get; set; } = "";
    public string? Description { get; set; }
    public DiscountType DiscountType { get; set; } = DiscountType.Percent;
    public decimal DiscountValue { get; set; } = 0;
    public PromotionType PromotionType { get; set; } = PromotionType.Promotion;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal MinOrderAmount { get; set; } = 0;
    public int UsageLimit { get; set; } = 0;
    public PromotionStatus Status { get; set; } = PromotionStatus.Active;
}