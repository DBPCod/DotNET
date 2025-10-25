using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class PromotionRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<Promotion?> HandleGetPromotionById(Guid id)
    {
        try
        {
            return await _context.Promotion
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<Promotion>> HandleGetAllPromotions()
    {
        try
        {
            return await _context.Promotion
                .OrderBy(p => p.StartDate)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<(List<Promotion> promotions, int totalCount)> HandleGetPromotionsWithPagination(
        int page, int pageSize, string? searchTerm = null, string? status = null, 
        DateTime? fromDate = null, DateTime? toDate = null)
    {
        try
        {
            var query = _context.Promotion.AsQueryable();

            // Search by promo code or description
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => 
                    p.PromoCode.Contains(searchTerm) || 
                    (p.Description != null && p.Description.Contains(searchTerm)));
            }

            // Filter by status
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(p => p.Status == status);
            }

            // Filter by date range
            if (fromDate.HasValue)
            {
                query = query.Where(p => p.StartDate >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                query = query.Where(p => p.EndDate <= toDate.Value);
            }

            var totalCount = await query.CountAsync();
            
            var promotions = await query
                .OrderBy(p => p.StartDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (promotions, totalCount);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Promotion> HandleCreatePromotion(Promotion promotion)
    {
        try
        {
            await _context.Promotion.AddAsync(promotion);
            await _context.SaveChangesAsync();
            return promotion;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Promotion> HandleUpdatePromotion(Promotion promotion)
    {
        try
        {
            _context.Promotion.Update(promotion);
            await _context.SaveChangesAsync();
            return promotion;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> HandleSoftDeletePromotion(Guid id)
    {
        try
        {
            var promotion = await _context.Promotion.FindAsync(id);
            if (promotion == null)
                return false;

            promotion.Status = "inactive";
            _context.Promotion.Update(promotion);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> HandleCheckPromotionUsedInOrders(Guid promotionId)
    {
        try
        {
            return await _context.Order
                .AnyAsync(o => o.PromoId == promotionId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> HandleCheckPromoCodeExists(string promoCode, Guid? excludeId = null)
    {
        try
        {
            var query = _context.Promotion.Where(p => p.PromoCode == promoCode);
            
            if (excludeId.HasValue)
            {
                query = query.Where(p => p.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Promotion?> HandleGetPromotionByCode(string promoCode)
    {
        try
        {
            return await _context.Promotion
                .FirstOrDefaultAsync(p => p.PromoCode == promoCode.ToUpper());
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> HandleIncrementUsedCount(Guid promotionId)
    {
        try
        {
            var promotion = await _context.Promotion.FindAsync(promotionId);
            if (promotion == null)
                return false;

            promotion.UsedCount++;
            _context.Promotion.Update(promotion);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> HandleDecrementUsedCount(Guid promotionId)
    {
        try
        {
            var promotion = await _context.Promotion.FindAsync(promotionId);
            if (promotion == null || promotion.UsedCount <= 0)
                return false;

            promotion.UsedCount--;
            _context.Promotion.Update(promotion);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
