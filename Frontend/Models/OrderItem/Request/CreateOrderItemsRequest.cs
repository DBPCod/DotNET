public class CreateOrderItemsRequest
{
    public Guid OrderId { get; set; }  // Lấy từ bước 1
    public List<OrderItemInput> Items { get; set; } = new();
}

public class OrderItemInput
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}