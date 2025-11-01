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
                // Promotions
                new Promotion { PromoCode = "SALE10", Description = "Giảm 10% cho mọi đơn hàng", DiscountType = "percent", DiscountValue = 10, PromotionType = "promotion", StartDate = new DateTime(2025, 1, 1), EndDate = new DateTime(2025, 12, 31), MinOrderAmount = 0, UsageLimit = 0, Status = "active" },
                new Promotion { PromoCode = "FREESHIP50K", Description = "Giảm 50,000 cho đơn từ 300,000 trở lên", DiscountType = "fixed", DiscountValue = 50000, PromotionType = "promotion", StartDate = new DateTime(2025, 3, 1), EndDate = new DateTime(2025, 12, 31), MinOrderAmount = 300000, UsageLimit = 500, Status = "active" },
                new Promotion { PromoCode = "NEWUSER", Description = "Giảm 20% cho khách hàng mới", DiscountType = "percent", DiscountValue = 20, PromotionType = "promotion", StartDate = new DateTime(2025, 1, 1), EndDate = new DateTime(2025, 6, 30), MinOrderAmount = 0, UsageLimit = 1, Status = "active" },
                new Promotion { PromoCode = "SUMMER15", Description = "Giảm 15% mùa hè", DiscountType = "percent", DiscountValue = 15, PromotionType = "promotion", StartDate = new DateTime(2025, 6, 1), EndDate = new DateTime(2025, 8, 31), MinOrderAmount = 50000, UsageLimit = 1000, Status = "active" },
                new Promotion { PromoCode = "VIP100K", Description = "Giảm 100,000 cho đơn từ 1 triệu", DiscountType = "fixed", DiscountValue = 100000, PromotionType = "promotion", StartDate = new DateTime(2025, 1, 1), EndDate = new DateTime(2025, 12, 31), MinOrderAmount = 1000000, UsageLimit = 200, Status = "active" },
                
                // Discount Codes
                new Promotion { PromoCode = "BLACKFRIDAY50", Description = "Giảm giá 50% cho tất cả sản phẩm điện tử", DiscountType = "percent", DiscountValue = 50, PromotionType = "discount_code", StartDate = new DateTime(2025, 11, 1), EndDate = new DateTime(2025, 11, 30), MinOrderAmount = 100000, UsageLimit = 1000, Status = "active" },
                new Promotion { PromoCode = "WELCOME10", Description = "Giảm $10 cho khách hàng mới", DiscountType = "fixed", DiscountValue = 10, PromotionType = "discount_code", StartDate = new DateTime(2025, 1, 1), EndDate = new DateTime(2025, 12, 31), MinOrderAmount = 50000, UsageLimit = 500, Status = "active" },
                new Promotion { PromoCode = "FREESHIP", Description = "Miễn phí vận chuyển", DiscountType = "free_shipping", DiscountValue = 0, PromotionType = "discount_code", StartDate = new DateTime(2025, 1, 1), EndDate = new DateTime(2025, 12, 31), MinOrderAmount = 30000, UsageLimit = 200, Status = "active" }
            };
            await context.Promotion.AddRangeAsync(promotions);
        }
    }
}