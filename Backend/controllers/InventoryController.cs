using Backend.Dtos;
using Backend.Dtos.Requests;
using Backend.Services.Apis;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/v1/inventories")]
[ApiController]
public class InventoryController(InventoryService inventoryService) : ControllerBase
{
    private readonly InventoryService _inventoryService = inventoryService;

    // GET: api/v1/inventories - List with pagination and filters
    [HttpGet]
    public async Task<IActionResult> GetInventories(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? q = null,
        [FromQuery] string? status = null,
        [FromQuery] Guid? categoryId = null,
        [FromQuery] int? minQuantity = null)
    {
        try
        {
            var (inventories, totalCount) = await _inventoryService.GetAllAsync(
                page, pageSize, q, status, categoryId, minQuantity);

            var response = new
            {
                StatusCode = 200,
                Message = "Success",
                Data = new
                {
                    Inventories = inventories,
                    Pagination = new
                    {
                        CurrentPage = page,
                        PageSize = pageSize,
                        TotalCount = totalCount,
                        TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
                    }
                }
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    // GET: api/v1/inventories/{id} - Get by ID
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetInventoryById(Guid id)
    {
        try
        {
            var inventory = await _inventoryService.GetByIdAsync(id);
            if (inventory == null)
                return NotFound(new { message = "Inventory not found" });

            return Ok(new
            {
                StatusCode = 200,
                Message = "Success",
                Data = new { Inventory = inventory }
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    // GET: api/v1/inventories/product/{productId} - Get by Product ID
    [HttpGet("product/{productId:guid}")]
    public async Task<IActionResult> GetInventoryByProductId(Guid productId)
    {
        try
        {
            var inventory = await _inventoryService.GetByProductIdAsync(productId);
            if (inventory == null)
                return NotFound(new { message = "Inventory not found for this product" });

            return Ok(new
            {
                StatusCode = 200,
                Message = "Success",
                Data = new { Inventory = inventory }
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    // POST: api/v1/inventories - Create new inventory
    [HttpPost]
    public async Task<IActionResult> CreateInventory([FromBody] CreateInventoryRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var inventory = await _inventoryService.CreateAsync(request);
            var dto = await _inventoryService.GetByIdAsync(inventory.Id);

            return CreatedAtAction(nameof(GetInventoryById), new { id = inventory.Id }, new
            {
                StatusCode = 201,
                Message = "Inventory created successfully",
                Data = new { Inventory = dto }
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    // PUT: api/v1/inventories/{id} - Update inventory
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateInventory(Guid id, [FromBody] UpdateInventoryRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var inventory = await _inventoryService.UpdateAsync(id, request);
            return Ok(new
            {
                StatusCode = 200,
                Message = "Inventory updated successfully",
                Data = new { Inventory = inventory }
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    // POST: api/v1/inventories/update (Legacy endpoint)
    [HttpPost("update")]
    public async Task<IActionResult> UpdateInventoryLegacy([FromBody] UpdateInventoryRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _inventoryService.UpdateInventoryAsync(request);
            return Ok(new { message = "Inventory updated successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    // DELETE: api/v1/inventories/{id} - Delete inventory
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteInventory(Guid id)
    {
        try
        {
            var result = await _inventoryService.DeleteAsync(id);
            if (!result)
                return NotFound(new { message = "Inventory not found" });

            return Ok(new
            {
                StatusCode = 200,
                Message = "Inventory deleted successfully"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    // GET: api/v1/inventories/stats - Get inventory statistics
    [HttpGet("stats")]
    public async Task<IActionResult> GetInventoryStats()
    {
        try
        {
            var stats = await _inventoryService.GetStatsAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Success",
                Data = stats
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}


