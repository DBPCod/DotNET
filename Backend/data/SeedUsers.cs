using Microsoft.EntityFrameworkCore;
using Backend.Contexts;
using Backend.Models;

namespace Backend.Data;

public static class SeedUsers
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (!await context.User.AnyAsync())
        {
            var users = new List<User>
            {
                new User { Username = "admin", Email = "admin@example.com", Password = "123456", FullName = "Quản trị viên", Role = UserRole.ADMIN },
                new User { Username = "staff01", Email = "staff01@example.com", Password = "123456", FullName = "Nguyễn Văn A", Role = UserRole.STAFF },
                new User { Username = "staff02", Email = "staff02@example.com", Password = "123456", FullName = "Lê Thị B", Role = UserRole.STAFF }
            };
            await context.User.AddRangeAsync(users);
        }
    }
}