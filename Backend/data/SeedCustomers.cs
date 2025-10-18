using Microsoft.EntityFrameworkCore;
using Backend.Contexts;
using Backend.Models;

namespace Backend.Data;

public static class SeedCustomers
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (!await context.Customer.AnyAsync())
        {
            var customers = new List<Customer>
            {
                new Customer { Name = "Khách hàng 1", Phone = "0909000001", Email = "kh1@mail.com", Address = "Địa chỉ 1" },
                new Customer { Name = "Khách hàng 2", Phone = "0909000002", Email = "kh2@mail.com", Address = "Địa chỉ 2" },
                new Customer { Name = "Khách hàng 3", Phone = "0909000003", Email = "kh3@mail.com", Address = "Địa chỉ 3" },
                new Customer { Name = "Khách hàng 4", Phone = "0909000004", Email = "kh4@mail.com", Address = "Địa chỉ 4" },
                new Customer { Name = "Khách hàng 5", Phone = "0909000005", Email = "kh5@mail.com", Address = "Địa chỉ 5" },
                new Customer { Name = "Khách hàng 6", Phone = "0909000006", Email = "kh6@mail.com", Address = "Địa chỉ 6" },
                new Customer { Name = "Khách hàng 7", Phone = "0909000007", Email = "kh7@mail.com", Address = "Địa chỉ 7" },
                new Customer { Name = "Khách hàng 8", Phone = "0909000008", Email = "kh8@mail.com", Address = "Địa chỉ 8" },
                new Customer { Name = "Khách hàng 9", Phone = "0909000009", Email = "kh9@mail.com", Address = "Địa chỉ 9" },
                new Customer { Name = "Khách hàng 10", Phone = "0909000010", Email = "kh10@mail.com", Address = "Địa chỉ 10" },
                new Customer { Name = "Khách hàng 11", Phone = "0909000011", Email = "kh11@mail.com", Address = "Địa chỉ 11" },
                new Customer { Name = "Khách hàng 12", Phone = "0909000012", Email = "kh12@mail.com", Address = "Địa chỉ 12" },
                new Customer { Name = "Khách hàng 13", Phone = "0909000013", Email = "kh13@mail.com", Address = "Địa chỉ 13" },
                new Customer { Name = "Khách hàng 14", Phone = "0909000014", Email = "kh14@mail.com", Address = "Địa chỉ 14" },
                new Customer { Name = "Khách hàng 15", Phone = "0909000015", Email = "kh15@mail.com", Address = "Địa chỉ 15" },
                new Customer { Name = "Khách hàng 16", Phone = "0909000016", Email = "kh16@mail.com", Address = "Địa chỉ 16" },
                new Customer { Name = "Khách hàng 17", Phone = "0909000017", Email = "kh17@mail.com", Address = "Địa chỉ 17" },
                new Customer { Name = "Khách hàng 18", Phone = "0909000018", Email = "kh18@mail.com", Address = "Địa chỉ 18" },
                new Customer { Name = "Khách hàng 19", Phone = "0909000019", Email = "kh19@mail.com", Address = "Địa chỉ 19" },
                new Customer { Name = "Khách hàng 20", Phone = "0909000020", Email = "kh20@mail.com", Address = "Địa chỉ 20" }
            };
            await context.Customer.AddRangeAsync(customers);
        }
    }
}