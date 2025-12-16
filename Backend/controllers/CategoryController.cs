using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Services.Apis;
using Backend.Dtos.Requests;

namespace Backend.Controllers;

[Route("api/v1/categories")]
[ApiController]
public class CategoryController(CategoryService categoryService) : ControllerBase
{
    private readonly CategoryService _categoryService = categoryService;
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? q = null)
    {
        var response = await _categoryService.GetAllAsync(page, pageSize, q);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var response = await _categoryService.GetByIdAsync(id);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    [Authorize(Roles = "STAFF,ADMIN")]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
    {
        var response = await _categoryService.CreateAsync(request);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "STAFF,ADMIN")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCategoryRequest request)
    {
        var response = await _categoryService.UpdateAsync(id, request);
        return StatusCode(response.StatusCode, response);
    }

    // Soft Delete
    [HttpDelete("{id}")]
    [Authorize(Roles = "STAFF,ADMIN")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var response = await _categoryService.DeleteAsync(id);
        return StatusCode(response.StatusCode, response);
    }

    // Restore Category (optional)
    [HttpPatch("{id}/restore")]
    [Authorize(Roles = "STAFF,ADMIN")]
    public async Task<IActionResult> Restore([FromRoute] Guid id)
    {
        var response = await _categoryService.RestoreAsync(id);
        return StatusCode(response.StatusCode, response);
    }
}