using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Dtos.Requests.Promotion;
using Backend.Dtos.Responses;
using Backend.Utils.Customs;
using Backend.Dtos.Requests.Order;
namespace Backend.Controllers;

[Route("api/orders")]
[ApiController]
[Authorize]
public class OrderController(OrderService orderService, PromotionService promotionService) : ControllerBase
{
    private readonly OrderService _orderService = orderService;
    private readonly PromotionService _promotionService = promotionService;

    // POST /api/orders/{orderId}/apply-promo - Áp dụng mã khuyến mãi cho order
    [HttpPost("{orderId}/apply-promo")]
    [Authorize(Roles = "STAFF,ADMIN")] // Staff và Admin được áp promo
    public async Task<IActionResult> ApplyPromo(Guid orderId, [FromBody] ApplyPromoRequest request)
    {
        var response = new Response();

        try
        {
            var result = await _promotionService.HandleApplyPromoToOrder(orderId, request.Code);
            
            response.Message = "Promotion applied successfully";
            response.StatusCode = 200;
            response.Data.ApplyPromoResult = result;
        }
        catch (ExceptionCustom ex)
        {
            response.Message = ex.Message;
            response.StatusCode = ex.StatusCode;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.StatusCode = 500;
        }

        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await _orderService.HandleGetAllOrder();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        var order = await _orderService.HandleGetOrderById(id);
        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> HandleCreateOrder([FromBody] CreateOrderRequest order)
    {
        if (!ModelState.IsValid)
        return BadRequest(ModelState);

        var createdOrder = await _orderService.HandleCreateOrder(order);
        return CreatedAtAction(nameof(GetOrderById), new {id = createdOrder.Id}, createdOrder);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> HandleDeleteOrder(Guid id)
    {
        var result = await _orderService.HandleDeleteOrder(id);
        if(!result)
        {
            return NotFound(new {message=$"Order với id = {id} không tồn tại!"});
        }
        return Ok(new {message=$"Xóa order thành công!"});
    }
}