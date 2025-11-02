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


    // GET: /api/v1/order-items
    [HttpGet]
    public async Task<IActionResult> GetAllOrderItems()
    {
        var items = await _orderItemService.GetAllOrderItemsAsync();
        return Ok(items);
    }

    // GET: /api/v1/order-items/{orderId}
    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrderItem(Guid orderId)
    {
        try
        {
            var item = await _orderItemService.GetOrderItemsByOrderIdAsync(orderId);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
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