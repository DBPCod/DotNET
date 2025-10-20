using Microsoft.EntityFrameworkCore;
using Backend.Contexts;
using Backend.Models;

namespace Backend.Data;

public static class SeedCategories
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (!await context.Category.AnyAsync())
        {
            var categories = new List<Category>
            {
                new Category { CategoryName = "Đồ uống" },
                new Category { CategoryName = "Bánh kẹo" },
                new Category { CategoryName = "Gia vị" },
                new Category { CategoryName = "Đồ gia dụng" },
                new Category { CategoryName = "Mỹ phẩm" }
            };
            await context.Category.AddRangeAsync(categories);
        }
    }
}