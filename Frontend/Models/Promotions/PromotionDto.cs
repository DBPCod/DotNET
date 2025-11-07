namespace Frontend.Models.Promotions;

public class PromotionDto
{
    public string Id { get; set; } = "";
    public string PromoCode { get; set; } = "";
    public string? Description { get; set; }
    public DiscountType DiscountType { get; set; } = DiscountType.Percent;
    public decimal DiscountValue { get; set; }
    public PromotionType PromotionType { get; set; } = PromotionType.Promotion;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal MinOrderAmount { get; set; }
    public int UsageLimit { get; set; }
    public int UsedCount { get; set; }
    public PromotionStatus Status { get; set; } = PromotionStatus.Active;
    public bool CanEdit { get; set; } = true;
}