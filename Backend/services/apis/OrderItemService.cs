using Backend.Dtos.Requests;

public class OrderItemService
{
    private readonly OrderItemRepository _orderItemRepository;

    public OrderItemService(OrderItemRepository orderItemRepository)
    {
        _orderItemRepository = orderItemRepository;
    }

    public async Task<List<OrderItem>> CreateOrderItemsAsync(CreateOrderItemsRequest request)
    {
        var orderItems = new List<OrderItem>();
        decimal totalAmount = 0;

        foreach (var input in request.Items)
        {
            var price = await _orderItemRepository.GetProductPriceAsync(input.ProductId);
            var subtotal = price * input.Quantity;

            var item = new OrderItem
            {
                OrderId = request.OrderId,
                ProductId = input.ProductId,
                Quantity = input.Quantity,
                Price = price,
                Subtotal = subtotal
            };

            orderItems.Add(item);
            totalAmount += subtotal;
        }

        // Lưu tất cả OrderItems
        await _orderItemRepository.AddOrderItemAsync(orderItems);

        // Cập nhật tổng tiền Order
        await _orderItemRepository.UpdateOrderTotalAsync(request.OrderId, totalAmount);

        return orderItems;
    }
}
