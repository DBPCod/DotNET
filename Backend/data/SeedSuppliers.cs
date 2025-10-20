using Microsoft.EntityFrameworkCore;
using Backend.Contexts;
using Backend.Models;

namespace Backend.Data;

public static class SeedSuppliers
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (!await context.Supplier.AnyAsync())
        {
            var suppliers = new List<Supplier>
            {
                new Supplier { Name = "Công ty ABC", Phone = "0909123456", Email = "abc@gmail.com", Address = "Hà Nội" },
                new Supplier { Name = "Công ty XYZ", Phone = "0912123456", Email = "xyz@gmail.com", Address = "TP HCM" },
                new Supplier { Name = "Công ty 123", Phone = "0933123456", Email = "123@gmail.com", Address = "Đà Nẵng" }
            };
            await context.Supplier.AddRangeAsync(suppliers);
        }
    }
}