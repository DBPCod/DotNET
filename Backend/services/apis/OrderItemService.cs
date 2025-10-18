namespace Backend.Services.Apis;

public class OrderItemService(OrderItemRepository orderItemRepository)
{
    private readonly OrderItemRepository _orderItemRepository = orderItemRepository;

}
