using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/v1/products")]
[ApiController]
public class ProductController(ProductService productService) : ControllerBase
{
    private readonly ProductService _productService = productService;

}