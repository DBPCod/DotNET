using Microsoft.EntityFrameworkCore;
using Backend.Contexts;
using Backend.Models;

namespace Backend.Data;

public static class SeedPromotions
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (!await context.Promotion.AnyAsync())
        {
            var promotions = new List<Promotion>
            {
                new Promotion { PromoCode = "SALE10", Description = "Giảm 10% cho mọi đơn hàng", DiscountType = "percent", DiscountValue = 10, StartDate = new DateTime(2025, 1, 1), EndDate = new DateTime(2025, 12, 31), MinOrderAmount = 0, UsageLimit = 0, Status = "active" },
                new Promotion { PromoCode = "FREESHIP50K", Description = "Giảm 50,000 cho đơn từ 300,000 trở lên", DiscountType = "fixed", DiscountValue = 50000, StartDate = new DateTime(2025, 3, 1), EndDate = new DateTime(2025, 12, 31), MinOrderAmount = 300000, UsageLimit = 500, Status = "active" },
                new Promotion { PromoCode = "NEWUSER", Description = "Giảm 20% cho khách hàng mới", DiscountType = "percent", DiscountValue = 20, StartDate = new DateTime(2025, 1, 1), EndDate = new DateTime(2025, 6, 30), MinOrderAmount = 0, UsageLimit = 1, Status = "active" },
                new Promotion { PromoCode = "SUMMER15", Description = "Giảm 15% mùa hè", DiscountType = "percent", DiscountValue = 15, StartDate = new DateTime(2025, 6, 1), EndDate = new DateTime(2025, 8, 31), MinOrderAmount = 50000, UsageLimit = 1000, Status = "active" },
                new Promotion { PromoCode = "VIP100K", Description = "Giảm 100,000 cho đơn từ 1 triệu", DiscountType = "fixed", DiscountValue = 100000, StartDate = new DateTime(2025, 1, 1), EndDate = new DateTime(2025, 12, 31), MinOrderAmount = 1000000, UsageLimit = 200, Status = "active" }
            };
            await context.Promotion.AddRangeAsync(promotions);
        }
    }
}