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
                new Category { CategoryName = "Đồ uống", Description = "Nước ngọt, nước có ga, cà phê, trà và các loại thức uống khác" },
                new Category { CategoryName = "Bánh kẹo", Description = "Các loại bánh, kẹo, snack và đồ ăn vặt" },
                new Category { CategoryName = "Gia vị", Description = "Các loại gia vị nấu ăn, nước chấm, dầu ăn" },
                new Category { CategoryName = "Đồ gia dụng", Description = "Các vật dụng sinh hoạt trong nhà" },
                new Category { CategoryName = "Mỹ phẩm", Description = "Sản phẩm chăm sóc cá nhân và làm đẹp" }
            };
            await context.Category.AddRangeAsync(categories);
        }
    }
}