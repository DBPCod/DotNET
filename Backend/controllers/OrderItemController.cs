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
    // [HttpGet]
    // public async Task<IActionResult> GetAllOrderItems()
    // {
    //     var items = await _orderItemService.GetAllOrderItemsAsync();
    //     return Ok(items);
    // }

    // GET /api/v1/order-items - Lấy danh sách order items với phân trang và filter (tương tự OrderController)
    [HttpGet]
    public async Task<IActionResult> GetOrderItems([FromQuery] GetOrderItemRequest request)
    {
        var response = new Response();

        try
        {
            var (orderItems, totalCount) = await _orderItemService.HandleGetOrdersWithPagination(
                request.Page,
                request.PageSize
            );

            var orderItemDtos = OrderItemMapper.MapListEntityToListDto(orderItems);
            var totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);

            response.Message = "Order items retrieved successfully";
            response.StatusCode = 200;
            response.Data.orderItems = orderItemDtos;
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
    // GET: /api/v1/order-items/{orderId} - Lấy order items theo orderId với phân trang (tương tự GetOrderItems)
    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrderItemsByOrderId(Guid orderId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var response = new Response();

        try
        {
            var (orderItems, totalCount) = await _orderItemService.HandleGetOrderItemsByOrderIdWithPagination(
                orderId, page, pageSize
            );

            var orderItemDtos = OrderItemMapper.MapListEntityToListDto(orderItems);
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            response.Message = "Order items retrieved successfully";
            response.StatusCode = 200;
            response.Data.orderItems = orderItemDtos;
            response.Data.Pagination = new PaginationInfo
            {
                CurrentPage = page,
                PageSize = pageSize,
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