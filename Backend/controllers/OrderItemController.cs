using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/v1/order-items")]
[ApiController]
public class OrderItemController(OrderItemService orderItemService) : ControllerBase
{
    private readonly OrderItemService _orderItemService = orderItemService;

}