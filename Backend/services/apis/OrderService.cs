namespace Backend.Services.Apis;
using Backend.Dtos.Requests.Order;
using Backend.Dtos.Responses;

public class OrderService(OrderRepository orderRepository)
{
    private readonly OrderRepository _orderRepository = orderRepository;

    public async Task<List<Order>> HandleGetAllOrder()
    {
        try
        {
            return await _orderRepository.HandleGetAllOrder();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Order> HandleGetOrderById(Guid id)
    {
        try
        {
            return await _orderRepository.HandleGetOrderById(id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<OrderDto> HandleCreateOrder(CreateOrderRequest request)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
        if (customer == null)
            throw new Exception("Customer not found");

        var user = await _userRepository.HandleGetUserByEmail(request.Email);
        if (user == null)
            throw new Exception("User not found");

        if (request.DiscountAmount > request.TotalAmount)
            throw new Exception("Discount cannot exceed total amount");

        var order = new Order
        {
            CustomerId = request.CustomerId,
            UserId = request.UserId,
            PromoId = request.PromoId,
            TotalAmount = request.TotalAmount,
            DiscountAmount = request.DiscountAmount,
            Status = "pending",
            OrderDate = DateTime.Now
        };

        var created = await _orderRepository.CreateOrderAsync(order);

        return new OrderDto
        {
            Id = created.Id,
            UserId = created.UserId,
            CustomerId = created.CustomerId.Value,
            PromoId = created.PromoId,
            OrderDate = created.OrderDate,
            Status = created.Status,
            TotalAmount = created.TotalAmount,
            DiscountAmount = created.DiscountAmount
        };
    }

    public async Task<bool> HandleDeleteOrder(Guid id)
    {
        try
        {
            return await _orderRepository.HandleDeleteOrder(id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}
