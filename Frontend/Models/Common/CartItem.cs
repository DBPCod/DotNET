namespace Frontend.Models.Common;

public class CartItem
{
    public string ProductId { get; set; } = "";
    public string ProductName { get; set; } = "";
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public string? ImagePath { get; set; }

    public decimal Total => UnitPrice * Quantity;
}


