using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/v1/order-items")]
[ApiController]
public class OrderItemController : ControllerBase
{
    private readonly OrderItemService _orderItemService;

    public OrderItemController(OrderItemService orderItemService)
    {
        _orderItemService = orderItemService;
    }


    [HttpPost]
    public async Task<IActionResult> CreateOrderItems([FromBody] CreateOrderItemsRequest request)
    {
        try
        {
            var addedItems = await _orderItemService.CreateOrderItemsAsync(request);
            return Ok(new
            {
                message = "Order items added successfully",
                totalItems = addedItems.Count,
                items = addedItems
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}