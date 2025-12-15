using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Services.Apis;
using Backend.Dtos.Requests; 

namespace Backend.Controllers;

[Route("api/v1/suppliers")]
[ApiController]
public class SupplierController(SupplierService supplierService) : ControllerBase
{
    private readonly SupplierService _supplierService = supplierService;

    [HttpGet]
    [Authorize(Roles = "STAFF,ADMIN")]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var response = await _supplierService.GetAllAsync(page, pageSize);
        return StatusCode(response.StatusCode, response);
    }

    // Endpoint mới để lấy chỉ suppliers đang hoạt động
    [HttpGet("active")]
    [Authorize(Roles = "STAFF,ADMIN")]
    public async Task<IActionResult> GetActive()
    {
        var response = await _supplierService.GetActiveAsync();
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "STAFF,ADMIN")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var response = await _supplierService.GetByIdAsync(id);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    [Authorize(Roles = "STAFF,ADMIN")]
    public async Task<IActionResult> Create([FromBody] CreateSupplierRequest request)
    {
        var response = await _supplierService.CreateAsync(request);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "STAFF,ADMIN")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateSupplierRequest request)
    {
        var response = await _supplierService.UpdateAsync(id, request);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPatch("{id}/toggle-status")]
    [Authorize(Roles = "STAFF,ADMIN")]
    public async Task<IActionResult> ToggleStatus([FromRoute] Guid id)
    {
        var response = await _supplierService.ToggleStatusAsync(id);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "STAFF,ADMIN")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var response = await _supplierService.DeleteAsync(id);
        return StatusCode(response.StatusCode, response);
    }
}