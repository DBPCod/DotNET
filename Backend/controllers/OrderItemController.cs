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
            response.Data.OrderItemList = orderItemDtos;
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
            response.Data.OrderItemList = orderItemDtos;
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
        var response = new Response();
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var addedItems = await _orderItemService.CreateOrderItemsAsync(request);
            if (addedItems == null || !addedItems.Any())
            {
                response.Message = "Failed to create order items";
                response.StatusCode = 500;
                return StatusCode(response.StatusCode, response);
            }

            var orderItemsDto = OrderItemMapper.MapListEntityToListDto(addedItems); // Sửa: Dùng đúng mapper cho list OrderItem
            response.Message = "Order items added successfully";
            response.StatusCode = 201;
            response.Data.OrderItemList = orderItemsDto; // Sửa: Gán DTOs thay vì entities
            return StatusCode(response.StatusCode, response); // Sửa: Trả về Response object thay vì anonymous object
        }
        catch (ExceptionCustom ex)
        {
            return BadRequest(ex.Message); // Thêm: Catch ExceptionCustom trước
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}