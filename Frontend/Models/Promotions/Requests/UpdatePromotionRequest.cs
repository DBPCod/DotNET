namespace Frontend.Models.Promotions.Requests;

public class UpdatePromotionRequest
{
    public string? Description { get; set; }
    public DiscountType? DiscountType { get; set; }
    public decimal? DiscountValue { get; set; }
    public PromotionType? PromotionType { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? MinOrderAmount { get; set; }
    public int? UsageLimit { get; set; }
    public PromotionStatus? Status { get; set; }
}