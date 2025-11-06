namespace Frontend.Models.Promotions.Requests;

public class CreatePromotionRequest
{
    public string PromoCode { get; set; } = "";
    public string? Description { get; set; }
    public string DiscountType { get; set; } = "";
    public decimal DiscountValue { get; set; } = 0;
    public string PromotionType { get; set; } = "promotion";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal MinOrderAmount { get; set; } = 0;
    public int UsageLimit { get; set; } = 0;
    public string Status { get; set; } = "active";
}