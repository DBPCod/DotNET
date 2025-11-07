namespace Frontend.Models.OrderItem.Responses;
using Frontend.Models;
using Frontend.Models.Common;
public class OrderItemListResponse
{
    public List<OrderItemDto> orderItems { get; set; } = new();
    public PaginationInfo Pagination { get; set; } = new();
}