using Microsoft.AspNetCore.Mvc;
using Backend.Services.Apis;
using Backend.Dtos.Requests;

namespace Backend.Controllers;

[Route("api/v1/products")]
[ApiController]
public class ProductController(ProductService productService) : ControllerBase
{
    private readonly ProductService _productService = productService;

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 20,
        [FromQuery] string? q = null,
        [FromQuery] string? categoryId = null,
        [FromQuery] string? supplierId = null,
        [FromQuery] bool? status = null)
    {
        var response = await _productService.GetAllAsync(page, pageSize, q, categoryId, supplierId, status);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var response = await _productService.GetByIdAsync(id);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateProductRequest request)
    {
        var response = await _productService.CreateAsync(request);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] UpdateProductRequest request)
    {
        var response = await _productService.UpdateAsync(id, request);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var response = await _productService.DeleteAsync(id);
        return StatusCode(response.StatusCode, response);
    }
}