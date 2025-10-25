using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Dtos.Requests.Promotion;
using Backend.Dtos.Responses;
using Backend.Utils.Customs;

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
}