using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/v1/inventories")]
[ApiController]
public class InventoryController(InventoryService inventoryService) : ControllerBase
{
    private readonly InventoryService _inventoryService = inventoryService;

}