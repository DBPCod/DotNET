namespace Frontend.Models.Order.Responses;
using Frontend.Models.Common;


public class OrderListResponse
{
    public List<OrderDto> Orders { get; set; } = new();
    public PaginationInfo Pagination { get; set; } = new();
}