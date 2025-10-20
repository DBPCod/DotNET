namespace Backend.Dtos;

public class InventoryDto
{
    public string Id { get; set; } = "";
    public string ProductId { get; set; } = "";
    public int Quantity { get; set; }
    public DateTime UpdatedAt { get; set; }
}