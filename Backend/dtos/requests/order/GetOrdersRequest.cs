namespace Backend.Dtos.Requests.Order;

public class GetOrdersRequest
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Q { get; set; } // Search query
    public string? Status { get; set; } // Filter by status

    // Thêm mới: Params cho filter ngày (optional, bind từ query)
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}