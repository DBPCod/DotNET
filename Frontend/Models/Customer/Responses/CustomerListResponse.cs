using Frontend.Models.Common;

namespace Frontend.Models.Customer.Responses;

public class CustomerListResponse
{
    public List<CustomerDto> Customers { get; set; } = new();
    public PaginationInfo? Pagination { get; set; }
}