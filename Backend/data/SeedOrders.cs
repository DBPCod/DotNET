using Microsoft.EntityFrameworkCore;
using Backend.Contexts;
using Backend.Models;

namespace Backend.Data;

public static class SeedOrders
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (!await context.Order.AnyAsync())
        {
            // Load data cần thiết trước
            var customers = await context.Customer.ToListAsync();
            var users = await context.User.ToListAsync();
            var promotions = await context.Promotion.ToListAsync();

            // Helper methods để tìm ID
            Guid GetCustomerId(string name) => customers.First(c => c.Name == name).Id;
            Guid GetUserId(string username) => users.First(u => u.Username == username).Id;
            Guid? GetPromoId(string code) => promotions.FirstOrDefault(p => p.PromoCode == code)?.Id;

            var orders = new List<Order>
            {
                new Order { CustomerId = GetCustomerId("Khách hàng 5"), UserId = GetUserId("staff02"), PromoId = GetPromoId("VIP100K"), Status = "paid", TotalAmount = 1292330, DiscountAmount = 100000 },
                new Order { CustomerId = GetCustomerId("Khách hàng 17"), UserId = GetUserId("staff02"), PromoId = null, Status = "paid", TotalAmount = 1731608, DiscountAmount = 0 },
                new Order { CustomerId = GetCustomerId("Khách hàng 8"), UserId = GetUserId("staff02"), PromoId = null, Status = "paid", TotalAmount = 720782, DiscountAmount = 0 },
                new Order { CustomerId = GetCustomerId("Khách hàng 20"), UserId = GetUserId("staff02"), PromoId = GetPromoId("VIP100K"), Status = "paid", TotalAmount = 21686, DiscountAmount = 21686 },
                new Order { CustomerId = GetCustomerId("Khách hàng 1"), UserId = GetUserId("staff01"), PromoId = null, Status = "paid", TotalAmount = 94180, DiscountAmount = 0 },
                new Order { CustomerId = GetCustomerId("Khách hàng 5"), UserId = GetUserId("staff02"), PromoId = GetPromoId("FREESHIP50K"), Status = "paid", TotalAmount = 3888671, DiscountAmount = 100000 },
                new Order { CustomerId = GetCustomerId("Khách hàng 9"), UserId = GetUserId("staff02"), PromoId = GetPromoId("SUMMER15"), Status = "paid", TotalAmount = 512594, DiscountAmount = 102518.8m },
                new Order { CustomerId = GetCustomerId("Khách hàng 11"), UserId = GetUserId("staff02"), PromoId = GetPromoId("NEWUSER"), Status = "paid", TotalAmount = 1715029, DiscountAmount = 171502.9m },
                new Order { CustomerId = GetCustomerId("Khách hàng 11"), UserId = GetUserId("staff02"), PromoId = null, Status = "paid", TotalAmount = 2484051, DiscountAmount = 0 },
                new Order { CustomerId = GetCustomerId("Khách hàng 11"), UserId = GetUserId("staff02"), PromoId = GetPromoId("FREESHIP50K"), Status = "paid", TotalAmount = 1070239, DiscountAmount = 100000 },
                new Order { CustomerId = GetCustomerId("Khách hàng 20"), UserId = GetUserId("staff02"), PromoId = null, Status = "paid", TotalAmount = 1532741, DiscountAmount = 0 },
                new Order { CustomerId = GetCustomerId("Khách hàng 10"), UserId = GetUserId("staff01"), PromoId = null, Status = "paid", TotalAmount = 1785354, DiscountAmount = 0 },
                new Order { CustomerId = GetCustomerId("Khách hàng 10"), UserId = GetUserId("staff02"), PromoId = GetPromoId("FREESHIP50K"), Status = "paid", TotalAmount = 1588276, DiscountAmount = 100000 },
                new Order { CustomerId = GetCustomerId("Khách hàng 6"), UserId = GetUserId("staff01"), PromoId = GetPromoId("FREESHIP50K"), Status = "paid", TotalAmount = 2896096, DiscountAmount = 50000 },
                new Order { CustomerId = GetCustomerId("Khách hàng 10"), UserId = GetUserId("staff01"), PromoId = GetPromoId("NEWUSER"), Status = "paid", TotalAmount = 186000, DiscountAmount = 27900 },
                new Order { CustomerId = GetCustomerId("Khách hàng 10"), UserId = GetUserId("staff01"), PromoId = GetPromoId("VIP100K"), Status = "paid", TotalAmount = 1024090, DiscountAmount = 50000 },
                new Order { CustomerId = GetCustomerId("Khách hàng 19"), UserId = GetUserId("staff02"), PromoId = null, Status = "paid", TotalAmount = 467148, DiscountAmount = 0 },
                new Order { CustomerId = GetCustomerId("Khách hàng 10"), UserId = GetUserId("staff01"), PromoId = null, Status = "paid", TotalAmount = 394342, DiscountAmount = 0 },
                new Order { CustomerId = GetCustomerId("Khách hàng 8"), UserId = GetUserId("staff02"), PromoId = GetPromoId("SUMMER15"), Status = "paid", TotalAmount = 1965637, DiscountAmount = 294845.55m },
                new Order { CustomerId = GetCustomerId("Khách hàng 3"), UserId = GetUserId("staff02"), PromoId = null, Status = "paid", TotalAmount = 2889813, DiscountAmount = 0 },
                new Order { CustomerId = GetCustomerId("Khách hàng 9"), UserId = GetUserId("staff01"), PromoId = null, Status = "paid", TotalAmount = 2288406, DiscountAmount = 0 },
                new Order { CustomerId = GetCustomerId("Khách hàng 17"), UserId = GetUserId("staff02"), PromoId = null, Status = "paid", TotalAmount = 331008, DiscountAmount = 0 },
                new Order { CustomerId = GetCustomerId("Khách hàng 6"), UserId = GetUserId("staff02"), PromoId = GetPromoId("SALE10"), Status = "paid", TotalAmount = 2154851, DiscountAmount = 323227.65m },
                new Order { CustomerId = GetCustomerId("Khách hàng 1"), UserId = GetUserId("staff02"), PromoId = GetPromoId("SALE10"), Status = "paid", TotalAmount = 1138686, DiscountAmount = 170802.9m },
                new Order { CustomerId = GetCustomerId("Khách hàng 2"), UserId = GetUserId("staff01"), PromoId = GetPromoId("VIP100K"), Status = "paid", TotalAmount = 393847, DiscountAmount = 100000 },
                new Order { CustomerId = GetCustomerId("Khách hàng 15"), UserId = GetUserId("staff02"), PromoId = GetPromoId("SALE10"), Status = "paid", TotalAmount = 260658, DiscountAmount = 52131.6m },
                new Order { CustomerId = GetCustomerId("Khách hàng 4"), UserId = GetUserId("staff01"), PromoId = null, Status = "paid", TotalAmount = 933199, DiscountAmount = 0 },
                new Order { CustomerId = GetCustomerId("Khách hàng 16"), UserId = GetUserId("staff01"), PromoId = null, Status = "paid", TotalAmount = 2609123, DiscountAmount = 0 },
                new Order { CustomerId = GetCustomerId("Khách hàng 4"), UserId = GetUserId("staff02"), PromoId = GetPromoId("SUMMER15"), Status = "paid", TotalAmount = 2406292, DiscountAmount = 481258.4m },
                new Order { CustomerId = GetCustomerId("Khách hàng 1"), UserId = GetUserId("staff02"), PromoId = null, Status = "paid", TotalAmount = 2912134, DiscountAmount = 0 }
            };
            await context.Order.AddRangeAsync(orders);
        }
    }
}