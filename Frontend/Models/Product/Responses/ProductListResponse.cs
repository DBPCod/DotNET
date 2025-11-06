using Frontend.Models.Common;

namespace Frontend.Models.Product.Responses;

public class ProductListResponse
{
    public List<ProductDto> Products { get; set; } = new();
    public PaginationInfo Pagination { get; set; } = new();
}
