using Microsoft.EntityFrameworkCore;
using Backend.Contexts;
using Backend.Models;

namespace Backend.Data;

public static class SeedData
{
    public static async Task SeedAsync(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Đảm bảo database được tạo
        await context.Database.EnsureCreatedAsync();

        // Seed data in order to respect foreign key dependencies
        await SeedUsers.SeedAsync(context);
        await SeedCustomers.SeedAsync(context);
        await SeedCategories.SeedAsync(context);
        await SeedSuppliers.SeedAsync(context);
        await SeedProducts.SeedAsync(context);
        await SeedInventory.SeedAsync(context);
        await SeedPromotions.SeedAsync(context);
        await SeedOrders.SeedAsync(context);
        await SeedOrderItems.SeedAsync(context);
        await SeedPayments.SeedAsync(context);

        await context.SaveChangesAsync();
    }
}