using Microsoft.AspNetCore.Mvc;
using Backend.Dtos.Requests;

namespace Backend.Controllers;

[Route("api/v1/customers")]
[ApiController]
public class CustomerController(Backend.Services.CustomerAppService customerService) : ControllerBase
{
    private readonly Backend.Services.CustomerAppService _customerService = customerService;
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var response = await _customerService.GetAllAsync(page, pageSize);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var response = await _customerService.GetByIdAsync(id);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerRequest request)
    {
        var response = await _customerService.CreateAsync(request);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCustomerRequest request)
    {
        var response = await _customerService.UpdateAsync(id, request);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var response = await _customerService.DeleteAsync(id);
        return StatusCode(response.StatusCode, response);
    }

}