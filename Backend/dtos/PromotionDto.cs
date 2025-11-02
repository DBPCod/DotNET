namespace Backend.Dtos;

public class PromotionDto
{
    public string Id { get; set; } = "";
    public string PromoCode { get; set; } = "";
    public string? Description { get; set; }
    public string DiscountType { get; set; } = "";
    public decimal DiscountValue { get; set; }
    public string PromotionType { get; set; } = "promotion";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal MinOrderAmount { get; set; }
    public int UsageLimit { get; set; }
    public int UsedCount { get; set; }
    public string Status { get; set; } = "";
    public bool CanEdit { get; set; } = true; // Có thể sửa promo_code hay không
}