namespace Frontend.Models.OrderItem.Responses;
using Frontend.Models;

/// <summary>
/// Response model cho CreateOrderItems API
/// Match với ResponseData structure từ backend
/// </summary>
public class CreateOrderItemsResponse
{
    public List<OrderItemDto> OrderItems { get; set; } = new();
}

