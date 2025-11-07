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

    public async Task<List<OrderItem>> GetAllOrderItemsAsync()
    {
        return await _orderItemRepository.GetAllOrderItemsAsync();
    }

    public async Task<(List<OrderItem> orderItems, int totalCount)> HandleGetOrdersWithPagination(
    int page, int pageSize)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;
        if (pageSize > 100) pageSize = 100;

        return await _orderItemRepository.HandleGetOrderItemsWithPagination(
            page, pageSize);
    }
    public async Task<(List<OrderItem> orderItems, int totalCount)> HandleGetOrderItemsByOrderIdWithPagination(
    Guid orderId, int page, int pageSize)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;
        if (pageSize > 100) pageSize = 100;

        return await _orderItemRepository.HandleGetOrderItemsByOrderIdWithPagination(
            orderId, page, pageSize);
    }


}
