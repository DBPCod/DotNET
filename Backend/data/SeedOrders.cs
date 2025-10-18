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
            var orders = new List<Order>
            {
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 5").Id, UserId = context.User.First(u => u.Username == "staff02").Id, PromoId = context.Promotion.First(p => p.PromoCode == "VIP100K").Id, Status = "paid", TotalAmount = 1292330, DiscountAmount = 100000 },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 17").Id, UserId = context.User.First(u => u.Username == "staff02").Id, PromoId = null, Status = "paid", TotalAmount = 1731608, DiscountAmount = 0 },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 8").Id, UserId = context.User.First(u => u.Username == "staff02").Id, PromoId = null, Status = "paid", TotalAmount = 720782, DiscountAmount = 0 },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 20").Id, UserId = context.User.First(u => u.Username == "staff02").Id, PromoId = context.Promotion.First(p => p.PromoCode == "VIP100K").Id, Status = "paid", TotalAmount = 21686, DiscountAmount = 21686 },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 1").Id, UserId = context.User.First(u => u.Username == "staff01").Id, PromoId = null, Status = "paid", TotalAmount = 94180, DiscountAmount = 0 },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 5").Id, UserId = context.User.First(u => u.Username == "staff02").Id, PromoId = context.Promotion.First(p => p.PromoCode == "FREESHIP50K").Id, Status = "paid", TotalAmount = 3888671, DiscountAmount = 100000 },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 9").Id, UserId = context.User.First(u => u.Username == "staff02").Id, PromoId = context.Promotion.First(p => p.PromoCode == "SUMMER15").Id, Status = "paid", TotalAmount = 512594, DiscountAmount = 102518.8m },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 11").Id, UserId = context.User.First(u => u.Username == "staff02").Id, PromoId = context.Promotion.First(p => p.PromoCode == "NEWUSER").Id, Status = "paid", TotalAmount = 1715029, DiscountAmount = 171502.9m },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 11").Id, UserId = context.User.First(u => u.Username == "staff02").Id, PromoId = null, Status = "paid", TotalAmount = 2484051, DiscountAmount = 0 },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 11").Id, UserId = context.User.First(u => u.Username == "staff02").Id, PromoId = context.Promotion.First(p => p.PromoCode == "FREESHIP50K").Id, Status = "paid", TotalAmount = 1070239, DiscountAmount = 100000 },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 20").Id, UserId = context.User.First(u => u.Username == "staff02").Id, PromoId = null, Status = "paid", TotalAmount = 1532741, DiscountAmount = 0 },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 10").Id, UserId = context.User.First(u => u.Username == "staff01").Id, PromoId = null, Status = "paid", TotalAmount = 1785354, DiscountAmount = 0 },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 10").Id, UserId = context.User.First(u => u.Username == "staff02").Id, PromoId = context.Promotion.First(p => p.PromoCode == "FREESHIP50K").Id, Status = "paid", TotalAmount = 1588276, DiscountAmount = 100000 },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 6").Id, UserId = context.User.First(u => u.Username == "staff01").Id, PromoId = context.Promotion.First(p => p.PromoCode == "FREESHIP50K").Id, Status = "paid", TotalAmount = 2896096, DiscountAmount = 50000 },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 10").Id, UserId = context.User.First(u => u.Username == "staff01").Id, PromoId = context.Promotion.First(p => p.PromoCode == "NEWUSER").Id, Status = "paid", TotalAmount = 186000, DiscountAmount = 27900 },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 10").Id, UserId = context.User.First(u => u.Username == "staff01").Id, PromoId = context.Promotion.First(p => p.PromoCode == "VIP100K").Id, Status = "paid", TotalAmount = 1024090, DiscountAmount = 50000 },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 19").Id, UserId = context.User.First(u => u.Username == "staff02").Id, PromoId = null, Status = "paid", TotalAmount = 467148, DiscountAmount = 0 },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 10").Id, UserId = context.User.First(u => u.Username == "staff01").Id, PromoId = null, Status = "paid", TotalAmount = 394342, DiscountAmount = 0 },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 8").Id, UserId = context.User.First(u => u.Username == "staff02").Id, PromoId = context.Promotion.First(p => p.PromoCode == "SUMMER15").Id, Status = "paid", TotalAmount = 1965637, DiscountAmount = 294845.55m },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 3").Id, UserId = context.User.First(u => u.Username == "staff02").Id, PromoId = null, Status = "paid", TotalAmount = 2889813, DiscountAmount = 0 },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 9").Id, UserId = context.User.First(u => u.Username == "staff01").Id, PromoId = null, Status = "paid", TotalAmount = 2288406, DiscountAmount = 0 },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 17").Id, UserId = context.User.First(u => u.Username == "staff02").Id, PromoId = null, Status = "paid", TotalAmount = 331008, DiscountAmount = 0 },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 6").Id, UserId = context.User.First(u => u.Username == "staff02").Id, PromoId = context.Promotion.First(p => p.PromoCode == "SALE10").Id, Status = "paid", TotalAmount = 2154851, DiscountAmount = 323227.65m },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 1").Id, UserId = context.User.First(u => u.Username == "staff02").Id, PromoId = context.Promotion.First(p => p.PromoCode == "SALE10").Id, Status = "paid", TotalAmount = 1138686, DiscountAmount = 170802.9m },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 2").Id, UserId = context.User.First(u => u.Username == "staff01").Id, PromoId = context.Promotion.First(p => p.PromoCode == "VIP100K").Id, Status = "paid", TotalAmount = 393847, DiscountAmount = 100000 },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 15").Id, UserId = context.User.First(u => u.Username == "staff02").Id, PromoId = context.Promotion.First(p => p.PromoCode == "SALE10").Id, Status = "paid", TotalAmount = 260658, DiscountAmount = 52131.6m },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 4").Id, UserId = context.User.First(u => u.Username == "staff01").Id, PromoId = null, Status = "paid", TotalAmount = 933199, DiscountAmount = 0 },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 16").Id, UserId = context.User.First(u => u.Username == "staff01").Id, PromoId = null, Status = "paid", TotalAmount = 2609123, DiscountAmount = 0 },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 4").Id, UserId = context.User.First(u => u.Username == "staff02").Id, PromoId = context.Promotion.First(p => p.PromoCode == "SUMMER15").Id, Status = "paid", TotalAmount = 2406292, DiscountAmount = 481258.4m },
                new Order { CustomerId = context.Customer.First(c => c.Name == "Khách hàng 1").Id, UserId = context.User.First(u => u.Username == "staff02").Id, PromoId = null, Status = "paid", TotalAmount = 2912134, DiscountAmount = 0 }
            };
            await context.Order.AddRangeAsync(orders);
        }
    }
}