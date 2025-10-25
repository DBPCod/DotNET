namespace Backend.Dtos.Responses;

public class ApplyPromoResponse
{
    public Guid OrderId { get; set; }
    public string PromoCode { get; set; } = "";
    public decimal DiscountAmount { get; set; }
    public decimal OrderTotalBefore { get; set; }
    public decimal OrderTotalAfter { get; set; }
}

