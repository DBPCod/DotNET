namespace Frontend.Models.Promotions.Responses;

public class ValidatePromotionResponse
{
    public bool Valid { get; set; }
    public string Reason { get; set; } = ""; // expired|inactive|min_order|usage_limit|not_found|ok
    public decimal DiscountAmount { get; set; }
    public string DiscountType { get; set; } = ""; // percent|fixed|free_shipping
    public string? PromotionId { get; set; } // ID của promotion nếu hợp lệ
}


