using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Dtos.Requests.Promotion;
using Backend.Utils.Mappers;
using Backend.Dtos.Responses;
using Backend.Utils.Customs;

namespace Backend.Controllers;

[Route("api/promotions")]
[ApiController]
public class PromotionController(PromotionService promotionService) : ControllerBase
{
    private readonly PromotionService _promotionService = promotionService;

    // POST /api/promotions - Tạo khuyến mãi mới (Staff & Admin)
    [HttpPost]
    [Authorize(Roles = "STAFF,ADMIN")]
    public async Task<IActionResult> CreatePromotion([FromBody] CreatePromotionRequest request)
    {
        var response = new Response();

        try
        {
            var promotion = await _promotionService.HandleCreatePromotion(request);
            if (promotion == null)
            {
                response.Message = "Failed to create promotion";
                response.StatusCode = 500;
                return StatusCode(response.StatusCode, response);
            }

            var promotionDto = PromotionMapper.MapEntityToDto(promotion);
            response.Message = "Promotion created successfully";
            response.StatusCode = 201;
            response.Data.Promotion = promotionDto;
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

    // GET /api/promotions - Lấy danh sách khuyến mãi với tìm kiếm/lọc (Staff & Admin)
    [HttpGet]
    [Authorize(Roles = "STAFF,ADMIN")]
    public async Task<IActionResult> GetPromotions([FromQuery] GetPromotionsRequest request)
    {
        var response = new Response();

        try
        {
            var (promotions, totalCount) = await _promotionService.HandleGetPromotionsWithPagination(
                request.Page,
                request.PageSize,
                request.Q,
                request.Status,
                request.DiscountType,
                request.PromotionType,
                request.From,
                request.To
            );

            // Check if promotions are used in orders for each promotion
            var promotionDtos = new List<PromotionDto>();
            foreach (var promotion in promotions)
            {
                var isUsedInOrders = await _promotionService.HandleCheckPromotionUsedInOrders(promotion.Id);
                var canEdit = !isUsedInOrders; // Không cho sửa nếu đã có order sử dụng
                promotionDtos.Add(PromotionMapper.MapEntityToDto(promotion, canEdit));
            }

            var totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);

            response.Message = "Promotions retrieved successfully";
            response.StatusCode = 200;
            response.Data.Promotions = promotionDtos;
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

    // GET /api/promotions/{id} - Lấy khuyến mãi theo ID (Staff & Admin)
    [HttpGet("{id}")]
    [Authorize(Roles = "STAFF,ADMIN")]
    public async Task<IActionResult> GetPromotion(Guid id)
    {
        var response = new Response();

        try
        {
            var promotion = await _promotionService.HandleGetPromotionById(id);
            if (promotion == null)
            {
                response.Message = "Promotion not found";
                response.StatusCode = 404;
                return StatusCode(response.StatusCode, response);
            }

            // Check if promotion is used in orders
            var isUsedInOrders = await _promotionService.HandleCheckPromotionUsedInOrders(id);
            var canEdit = !isUsedInOrders;

            var promotionDto = PromotionMapper.MapEntityToDto(promotion, canEdit);
            response.Message = "Promotion retrieved successfully";
            response.StatusCode = 200;
            response.Data.Promotion = promotionDto;
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

    // PUT /api/promotions/{id} - Cập nhật khuyến mãi (Staff & Admin)
    [HttpPut("{id}")]
    [Authorize(Roles = "STAFF,ADMIN")]
    public async Task<IActionResult> UpdatePromotion(Guid id, [FromBody] UpdatePromotionRequest request)
    {
        var response = new Response();

        try
        {
            var promotion = await _promotionService.HandleUpdatePromotion(id, request);
            if (promotion == null)
            {
                response.Message = "Promotion not found";
                response.StatusCode = 404;
                return StatusCode(response.StatusCode, response);
            }

            // Check if promotion is used in orders
            var isUsedInOrders = await _promotionService.HandleCheckPromotionUsedInOrders(id);
            var canEdit = !isUsedInOrders;

            var promotionDto = PromotionMapper.MapEntityToDto(promotion, canEdit);
            response.Message = "Promotion updated successfully";
            response.StatusCode = 200;
            response.Data.Promotion = promotionDto;
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

    // DELETE /api/promotions/{id} - Soft delete khuyến mãi (chuyển status = 'inactive') (Staff & Admin)
    [HttpDelete("{id}")]
    [Authorize(Roles = "STAFF,ADMIN")]
    public async Task<IActionResult> DeletePromotion(Guid id)
    {
        var response = new Response();

        try
        {
            var success = await _promotionService.HandleSoftDeletePromotion(id);
            if (!success)
            {
                response.Message = "Promotion not found";
                response.StatusCode = 404;
                return StatusCode(response.StatusCode, response);
            }

            response.Message = "Promotion deactivated successfully";
            response.StatusCode = 200;
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

    // GET /api/promotions/validate - Validate mã khuyến mãi (public cho khách hàng dùng mã)
    [HttpGet("validate")]
    [AllowAnonymous]
    public async Task<IActionResult> ValidatePromotion([FromQuery] ValidatePromotionRequest request)
    {
        var response = new Response();

        try
        {
            var result = await _promotionService.HandleValidatePromotion(request.Code, request.OrderTotal);
            
            response.Message = result.Valid ? "Promotion is valid" : $"Promotion is invalid: {result.Reason}";
            response.StatusCode = 200;
            response.Data.ValidationResult = result;
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