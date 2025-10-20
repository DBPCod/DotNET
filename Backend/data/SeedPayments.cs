using Microsoft.EntityFrameworkCore;
using Backend.Contexts;
using Backend.Models;

namespace Backend.Data;

public static class SeedPayments
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (!await context.Payment.AnyAsync())
        {
            var payments = new List<Payment>
            {
                new Payment { OrderId = context.Order.First().Id, Amount = 1292330, PaymentMethod = "credit_card", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(1).First().Id, Amount = 1731608, PaymentMethod = "bank_transfer", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(2).First().Id, Amount = 720782, PaymentMethod = "cash", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(3).First().Id, Amount = 21686, PaymentMethod = "credit_card", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(4).First().Id, Amount = 94180, PaymentMethod = "bank_transfer", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(5).First().Id, Amount = 3888671, PaymentMethod = "cash", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(6).First().Id, Amount = 512594, PaymentMethod = "credit_card", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(7).First().Id, Amount = 1715029, PaymentMethod = "bank_transfer", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(8).First().Id, Amount = 2484051, PaymentMethod = "cash", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(9).First().Id, Amount = 1070239, PaymentMethod = "credit_card", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(10).First().Id, Amount = 1532741, PaymentMethod = "bank_transfer", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(11).First().Id, Amount = 1785354, PaymentMethod = "cash", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(12).First().Id, Amount = 1588276, PaymentMethod = "credit_card", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(13).First().Id, Amount = 2896096, PaymentMethod = "bank_transfer", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(14).First().Id, Amount = 186000, PaymentMethod = "cash", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(15).First().Id, Amount = 1024090, PaymentMethod = "credit_card", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(16).First().Id, Amount = 467148, PaymentMethod = "bank_transfer", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(17).First().Id, Amount = 394342, PaymentMethod = "cash", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(18).First().Id, Amount = 1965637, PaymentMethod = "credit_card", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(19).First().Id, Amount = 2889813, PaymentMethod = "bank_transfer", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(20).First().Id, Amount = 2288406, PaymentMethod = "cash", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(21).First().Id, Amount = 331008, PaymentMethod = "credit_card", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(22).First().Id, Amount = 2154851, PaymentMethod = "bank_transfer", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(23).First().Id, Amount = 1138686, PaymentMethod = "cash", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(24).First().Id, Amount = 393847, PaymentMethod = "credit_card", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(25).First().Id, Amount = 260658, PaymentMethod = "bank_transfer", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(26).First().Id, Amount = 933199, PaymentMethod = "cash", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(27).First().Id, Amount = 2609123, PaymentMethod = "credit_card", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(28).First().Id, Amount = 2406292, PaymentMethod = "bank_transfer", PaymentDate = DateTime.Now },
                new Payment { OrderId = context.Order.Skip(29).First().Id, Amount = 2912134, PaymentMethod = "cash", PaymentDate = DateTime.Now }
            };
            await context.Payment.AddRangeAsync(payments);
        }
    }
}