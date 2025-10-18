namespace Backend.Services.Apis;

public class OrderService(OrderRepository orderRepository)
{
    private readonly OrderRepository _orderRepository = orderRepository;

}
