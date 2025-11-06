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
                new Promotion { PromoCode = "SALE10", Description = "Giảm 10% cho mọi đơn hàng", DiscountType = DiscountType.Percent, DiscountValue = 10, PromotionType = PromotionType.Promotion, StartDate = new DateTime(2025, 1, 1), EndDate = new DateTime(2025, 12, 31), MinOrderAmount = 0, UsageLimit = 0, Status = PromotionStatus.Active },
                new Promotion { PromoCode = "FREESHIP50K", Description = "Giảm 50,000 cho đơn từ 300,000 trở lên", DiscountType = DiscountType.Fixed, DiscountValue = 50000, PromotionType = PromotionType.Promotion, StartDate = new DateTime(2025, 3, 1), EndDate = new DateTime(2025, 12, 31), MinOrderAmount = 300000, UsageLimit = 500, Status = PromotionStatus.Active },
                new Promotion { PromoCode = "NEWUSER", Description = "Giảm 20% cho khách hàng mới", DiscountType = DiscountType.Percent, DiscountValue = 20, PromotionType = PromotionType.Promotion, StartDate = new DateTime(2025, 1, 1), EndDate = new DateTime(2025, 6, 30), MinOrderAmount = 0, UsageLimit = 1, Status = PromotionStatus.Active },
                new Promotion { PromoCode = "SUMMER15", Description = "Giảm 15% mùa hè", DiscountType = DiscountType.Percent, DiscountValue = 15, PromotionType = PromotionType.Promotion, StartDate = new DateTime(2025, 6, 1), EndDate = new DateTime(2025, 8, 31), MinOrderAmount = 50000, UsageLimit = 1000, Status = PromotionStatus.Active },
                new Promotion { PromoCode = "VIP100K", Description = "Giảm 100,000 cho đơn từ 1 triệu", DiscountType = DiscountType.Fixed, DiscountValue = 100000, PromotionType = PromotionType.Promotion, StartDate = new DateTime(2025, 1, 1), EndDate = new DateTime(2025, 12, 31), MinOrderAmount = 1000000, UsageLimit = 200, Status = PromotionStatus.Active },
                
                // Discount Codes
                new Promotion { PromoCode = "BLACKFRIDAY50", Description = "Giảm giá 50% cho tất cả sản phẩm điện tử", DiscountType = DiscountType.Percent, DiscountValue = 50, PromotionType = PromotionType.DiscountCode, StartDate = new DateTime(2025, 11, 1), EndDate = new DateTime(2025, 11, 30), MinOrderAmount = 100000, UsageLimit = 1000, Status = PromotionStatus.Active },
                new Promotion { PromoCode = "WELCOME10", Description = "Giảm $10 cho khách hàng mới", DiscountType = DiscountType.Fixed, DiscountValue = 10, PromotionType = PromotionType.DiscountCode, StartDate = new DateTime(2025, 1, 1), EndDate = new DateTime(2025, 12, 31), MinOrderAmount = 50000, UsageLimit = 500, Status = PromotionStatus.Active },
                new Promotion { PromoCode = "FREESHIP", Description = "Miễn phí vận chuyển", DiscountType = DiscountType.FreeShipping, DiscountValue = 0, PromotionType = PromotionType.DiscountCode, StartDate = new DateTime(2025, 1, 1), EndDate = new DateTime(2025, 12, 31), MinOrderAmount = 30000, UsageLimit = 200, Status = PromotionStatus.Active }
            };
            await context.Promotion.AddRangeAsync(promotions);
        }
    }
}