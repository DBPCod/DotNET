using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/v1/orders")]
[ApiController]
public class OrderController(OrderService orderService) : ControllerBase
{
    private readonly OrderService _orderService = orderService;

}