namespace Backend.Dtos;

public class OrderDto
{
    public string Id { get; set; } = "";
    public string? CustomerId { get; set; }
    public string? UserId { get; set; }
    public string? PromoId { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; } = "";
    public decimal? TotalAmount { get; set; }
    public decimal DiscountAmount { get; set; }
}