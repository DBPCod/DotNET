using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Dtos.Requests.Promotion;
using Backend.Dtos.Responses;
using Backend.Utils.Customs;
using Backend.Dtos.Requests.Order;
namespace Backend.Controllers;

[Route("api/v1/orders")]
[ApiController]
// [Authorize]
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

    // GET /api/v1/orders - Lấy danh sách orders với phân trang và filter (tương tự UserController)
    [HttpGet]
    // [Authorize(Roles = "STAFF,ADMIN")]
    public async Task<IActionResult> GetOrders([FromQuery] GetOrdersRequest request)
    {
        var response = new Response();

        try
        {
            var (orders, totalCount) = await _orderService.HandleGetOrdersWithPagination(
                request.Page,
                request.PageSize,
                request.Q,
                request.Status
            );

            var orderDtos = OrderMapper.MapListEntityToListDto(orders);
            var totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);

            response.Message = "Orders retrieved successfully";
            response.StatusCode = 200;
            response.Data.Orders = orderDtos;
            response.Data.Pagination = new PaginationInfo
            {
                CurrentPage = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
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

    // [HttpGet]
    // public async Task<IActionResult> GetAllOrders()
    // {
    //     var orders = await _orderService.HandleGetAllOrder();
    //     return Ok(orders);
    // }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        var order = await _orderService.HandleGetOrderById(id);
        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> HandleCreateOrder([FromBody] CreateOrderRequest order)
    {
        var response = new Response();
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var createdOrder = await _orderService.HandleCreateOrder(order);
            if (createdOrder == null)
            {
                response.Message = "Failed to create order";
                response.StatusCode = 500;
                return StatusCode(response.StatusCode, response);
            }

            var orderDto = OrderMapper.MapEntityToDto(createdOrder);
            response.Message = "Order created successfully";
            response.StatusCode = 201;
            response.Data.Order = orderDto;
        }
        catch (ExceptionCustom ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return StatusCode(response.StatusCode, response);
    }

    [HttpPatch("{id}/status")]
    // [Authorize(Roles = "STAFF,ADMIN")]
    public async Task<IActionResult> HandleUpdateStatus(Guid id, [FromBody] UpdateStatusOrderRequest updateStatusOrderRequest)
    {
        var response = new Response();
        try
        {
            var success = await _orderService.HandleUpdateStatus(id, updateStatusOrderRequest.status);
            response.Message = "Order status updated successfully";
            response.StatusCode = 200;
            response.Data.UpdateOrderStatus = new UpdateOrderStatusDto { OrderId = id, NewStatus = updateStatusOrderRequest.status };
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