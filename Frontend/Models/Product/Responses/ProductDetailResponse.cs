using Frontend.Models.Common;

namespace Frontend.Models.Product.Responses;



public class ProductDetailResponse
{
    public ProductDto Product { get; set; } = new();
}