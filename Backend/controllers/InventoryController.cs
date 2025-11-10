using Backend.Dtos.Requests;
using Backend.Services.Apis;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/v1/inventories")]
[ApiController]
public class InventoryController(InventoryService inventoryService) : ControllerBase
{
    private readonly InventoryService _inventoryService = inventoryService;

    [HttpPost("update")]
    public async Task<IActionResult> UpdateInventory([FromBody] UpdateInventoryRequest request)
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
}
