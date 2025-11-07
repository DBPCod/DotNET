namespace Backend.Services.Apis;
using Backend.Dtos.Requests.Order;
using Backend.Dtos.Responses;

public class OrderService(OrderRepository orderRepository, CustomerRepository customerRepository,UserRepository userRepository)
{
    private readonly OrderRepository _orderRepository = orderRepository;
    private readonly CustomerRepository _customerRepository = customerRepository;
    private readonly UserRepository _userRepository = userRepository;
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
    public async Task<(List<Order> orders, int totalCount)> HandleGetOrdersWithPagination(
    int page, int pageSize, string? searchTerm = null, string? status = null)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;
        if (pageSize > 100) pageSize = 100;

        return await _orderRepository.HandleGetOrdersWithPagination(
            page, pageSize, searchTerm, status);
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

    public async Task<Order> HandleCreateOrder(CreateOrderRequest request)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
        if (customer == null)
            throw new Exception("Customer not found");

        var user = await _userRepository.HandleGetUserById(request.UserId);
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

        var created = await _orderRepository.HandleCreateOrder(order);

        return created;
    }


    public async Task<bool> HandleUpdateStatus(Guid id, string newStatus)
    {
        var validStatuses = new[] { "pending", "paid", "canceled" };
        if (!validStatuses.Contains(newStatus.ToLower()))
        {
            throw new Exception("Invalid status");
        }

        var success = await _orderRepository.HandleUpdateStatus(id, newStatus);
        if (!success)
        {
            throw new Exception("Order not found");
        }

        return success;
    }
    public async Task<bool> HandleDeleteOrder(Guid id)
    {
        try
        {
            return await _orderRepository.HandleSoftDeleteOrder(id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}
