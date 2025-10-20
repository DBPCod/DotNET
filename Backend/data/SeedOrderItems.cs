using Microsoft.EntityFrameworkCore;
using Backend.Contexts;
using Backend.Models;

namespace Backend.Data;

public static class SeedOrderItems
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (!await context.OrderItem.AnyAsync())
        {
            var orderItems = new List<OrderItem>
            {
                // Add sample order items here based on orders and products
                new OrderItem { OrderId = context.Order.First().Id, ProductId = context.Product.First(p => p.Barcode == "8900000000001").Id, Quantity = 2, Price = 314838, Subtotal = 629676 },
                new OrderItem { OrderId = context.Order.First().Id, ProductId = context.Product.First(p => p.Barcode == "8900000000002").Id, Quantity = 1, Price = 114807, Subtotal = 114807 },
                // Add more order items as needed
            };
            await context.OrderItem.AddRangeAsync(orderItems);
        }
    }
}