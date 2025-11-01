namespace Backend.Dtos.Requests.Promotion;

public class GetPromotionsRequest
{
    public string? Q { get; set; } // Search by promo code or description
    public string? Status { get; set; } // Filter by status
    public string? DiscountType { get; set; } // Filter by discount type: 'percent', 'fixed', 'free_shipping'
    public string? PromotionType { get; set; } // Filter by promotion type: 'promotion' or 'discount_code'
    public DateTime? From { get; set; } // Filter by start date
    public DateTime? To { get; set; } // Filter by end date
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

