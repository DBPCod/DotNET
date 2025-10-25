using Backend.Dtos.Requests.Promotion;
using Backend.Dtos.Responses;
using Backend.Utils.Mappers;

namespace Backend.Services.Apis;

public class PromotionService(PromotionRepository promotionRepository, OrderRepository orderRepository, AppDbContext context)
{
    private readonly PromotionRepository _promotionRepository = promotionRepository;
    private readonly OrderRepository _orderRepository = orderRepository;
    private readonly AppDbContext _context = context;

    public async Task<Promotion?> HandleGetPromotionById(Guid id)
    {
        return await _promotionRepository.HandleGetPromotionById(id);
    }

    public async Task<(List<Promotion> promotions, int totalCount)> HandleGetPromotionsWithPagination(
        int page, int pageSize, string? searchTerm = null, string? status = null, 
        DateTime? fromDate = null, DateTime? toDate = null)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;
        if (pageSize > 100) pageSize = 100; // Giới hạn tối đa 100 records per page

        return await _promotionRepository.HandleGetPromotionsWithPagination(
            page, pageSize, searchTerm, status, fromDate, toDate);
    }

    public async Task<Promotion?> HandleCreatePromotion(CreatePromotionRequest request)
    {
        // Validate end_date >= start_date
        if (request.EndDate < request.StartDate)
            throw new ExceptionCustom(400, "End date must be greater than or equal to start date");

        // Validate discount value based on type
        if (request.DiscountType == "percent" && request.DiscountValue > 100)
            throw new ExceptionCustom(400, "Percentage discount cannot exceed 100%");

        // Check if promo code already exists
        var promoCodeExists = await _promotionRepository.HandleCheckPromoCodeExists(request.PromoCode);
        if (promoCodeExists)
            throw new ExceptionCustom(409, "Promo code already exists");

        var promotion = new Promotion
        {
            PromoCode = request.PromoCode.Trim().ToUpper(),
            Description = request.Description?.Trim(),
            DiscountType = request.DiscountType.ToLower(),
            DiscountValue = request.DiscountValue,
            StartDate = request.StartDate.Date,
            EndDate = request.EndDate.Date,
            MinOrderAmount = request.MinOrderAmount,
            UsageLimit = request.UsageLimit,
            Status = request.Status.ToLower()
        };

        return await _promotionRepository.HandleCreatePromotion(promotion);
    }

    public async Task<Promotion?> HandleUpdatePromotion(Guid id, UpdatePromotionRequest request)
    {
        var promotion = await _promotionRepository.HandleGetPromotionById(id);
        if (promotion == null)
            throw new ExceptionCustom(404, "Promotion not found");

        // Check if promotion is used in orders (không cho sửa promo_code)
        var isUsedInOrders = await _promotionRepository.HandleCheckPromotionUsedInOrders(id);

        // Validate end_date >= start_date if both are provided
        var startDate = request.StartDate ?? promotion.StartDate;
        var endDate = request.EndDate ?? promotion.EndDate;
        
        if (endDate < startDate)
            throw new ExceptionCustom(400, "End date must be greater than or equal to start date");

        // Validate discount value based on type
        var discountType = request.DiscountType ?? promotion.DiscountType;
        var discountValue = request.DiscountValue ?? promotion.DiscountValue;
        
        if (discountType == "percent" && discountValue > 100)
            throw new ExceptionCustom(400, "Percentage discount cannot exceed 100%");

        // Update fields
        if (!string.IsNullOrEmpty(request.Description))
            promotion.Description = request.Description.Trim();

        if (!string.IsNullOrEmpty(request.DiscountType))
            promotion.DiscountType = request.DiscountType.ToLower();

        if (request.DiscountValue.HasValue)
            promotion.DiscountValue = request.DiscountValue.Value;

        if (request.StartDate.HasValue)
            promotion.StartDate = request.StartDate.Value.Date;

        if (request.EndDate.HasValue)
            promotion.EndDate = request.EndDate.Value.Date;

        if (request.MinOrderAmount.HasValue)
            promotion.MinOrderAmount = request.MinOrderAmount.Value;

        if (request.UsageLimit.HasValue)
            promotion.UsageLimit = request.UsageLimit.Value;

        if (!string.IsNullOrEmpty(request.Status))
            promotion.Status = request.Status.ToLower();

        return await _promotionRepository.HandleUpdatePromotion(promotion);
    }

    public async Task<bool> HandleSoftDeletePromotion(Guid id)
    {
        var promotion = await _promotionRepository.HandleGetPromotionById(id);
        if (promotion == null)
            throw new ExceptionCustom(404, "Promotion not found");

        return await _promotionRepository.HandleSoftDeletePromotion(id);
    }

    public async Task<bool> HandleCheckPromotionUsedInOrders(Guid promotionId)
    {
        return await _promotionRepository.HandleCheckPromotionUsedInOrders(promotionId);
    }

    public async Task<ValidatePromotionResponse> HandleValidatePromotion(string code, decimal orderTotal)
    {
        var response = new ValidatePromotionResponse
        {
            Valid = false,
            Reason = "not_found",
            DiscountAmount = 0,
            DiscountType = ""
        };

        // Tìm promotion theo code
        var promotion = await _promotionRepository.HandleGetPromotionByCode(code);
        if (promotion == null)
        {
            response.Reason = "not_found";
            return response;
        }

        // Kiểm tra status
        if (promotion.Status != "active")
        {
            response.Reason = "inactive";
            return response;
        }

        // Kiểm tra ngày hiệu lực
        var today = DateTime.Today;
        if (today < promotion.StartDate || today > promotion.EndDate)
        {
            response.Reason = "expired";
            return response;
        }

        // Kiểm tra min_order_amount
        if (orderTotal < promotion.MinOrderAmount)
        {
            response.Reason = "min_order";
            return response;
        }

        // Kiểm tra usage_limit
        if (promotion.UsageLimit > 0 && promotion.UsedCount >= promotion.UsageLimit)
        {
            response.Reason = "usage_limit";
            return response;
        }

        // Tính discount amount
        decimal discountAmount = 0;
        if (promotion.DiscountType == "percent")
        {
            discountAmount = Math.Round(orderTotal * promotion.DiscountValue / 100, 2);
        }
        else if (promotion.DiscountType == "fixed")
        {
            discountAmount = Math.Min(promotion.DiscountValue, orderTotal);
        }

        response.Valid = true;
        response.Reason = "ok";
        response.DiscountAmount = discountAmount;
        response.DiscountType = promotion.DiscountType;

        return response;
    }

    public async Task<ApplyPromoResponse> HandleApplyPromoToOrder(Guid orderId, string code)
    {
        // Sử dụng transaction để đảm bảo tính nhất quán
        await using var transaction = await _context.Database.BeginTransactionAsync();
        
        try
        {
            // Lấy order
            var order = await _orderRepository.HandleGetOrderById(orderId);
            if (order == null)
                throw new ExceptionCustom(404, "Order not found");

            // Tính tổng đơn hàng (TotalAmount nếu có, nếu không lấy từ OrderItems)
            var orderTotalBefore = order.TotalAmount ?? 0;

            // Validate promotion
            var validation = await HandleValidatePromotion(code, orderTotalBefore);
            if (!validation.Valid)
            {
                var errorMessage = validation.Reason switch
                {
                    "not_found" => "Promotion code not found",
                    "inactive" => "Promotion is inactive",
                    "expired" => "Promotion has expired or not yet started",
                    "min_order" => "Order total does not meet minimum requirement",
                    "usage_limit" => "Promotion usage limit reached",
                    _ => "Invalid promotion"
                };
                throw new ExceptionCustom(400, errorMessage);
            }

            // Lấy promotion
            var promotion = await _promotionRepository.HandleGetPromotionByCode(code);
            if (promotion == null)
                throw new ExceptionCustom(404, "Promotion not found");

            // Lưu promo ID cũ để xử lý
            var oldPromoId = order.PromoId;

            // Nếu order đã có promo cũ khác, giảm used_count của promo cũ
            if (oldPromoId.HasValue && oldPromoId != promotion.Id)
            {
                await _promotionRepository.HandleDecrementUsedCount(oldPromoId.Value);
            }

            // Tính discount amount
            var discountAmount = validation.DiscountAmount;
            var orderTotalAfter = orderTotalBefore - discountAmount;

            // Cập nhật order
            order.PromoId = promotion.Id;
            order.DiscountAmount = discountAmount;
            // Nếu muốn lưu total sau giảm giá riêng thì có thể thêm field order.FinalAmount
            // Ở đây giữ nguyên TotalAmount, chỉ lưu DiscountAmount

            await _orderRepository.HandleUpdateOrder(order);

            // Tăng used_count nếu đổi sang promo mới (hoặc chưa có promo)
            if (!oldPromoId.HasValue || oldPromoId != promotion.Id)
            {
                await _promotionRepository.HandleIncrementUsedCount(promotion.Id);
            }

            await transaction.CommitAsync();

            return new ApplyPromoResponse
            {
                OrderId = orderId,
                PromoCode = promotion.PromoCode,
                DiscountAmount = discountAmount,
                OrderTotalBefore = orderTotalBefore,
                OrderTotalAfter = orderTotalAfter
            };
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
