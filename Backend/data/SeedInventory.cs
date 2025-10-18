using Microsoft.EntityFrameworkCore;
using Backend.Contexts;
using Backend.Models;

namespace Backend.Data;

public static class SeedInventory
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (!await context.Inventory.AnyAsync())
        {
            var inventory = new List<Inventory>
            {
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000001").Id, Quantity = 25 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000002").Id, Quantity = 169 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000003").Id, Quantity = 77 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000004").Id, Quantity = 169 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000005").Id, Quantity = 90 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000006").Id, Quantity = 105 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000007").Id, Quantity = 125 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000008").Id, Quantity = 37 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000009").Id, Quantity = 74 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000010").Id, Quantity = 149 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000011").Id, Quantity = 69 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000012").Id, Quantity = 23 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000013").Id, Quantity = 46 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000014").Id, Quantity = 144 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000015").Id, Quantity = 134 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000016").Id, Quantity = 182 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000017").Id, Quantity = 99 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000018").Id, Quantity = 72 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000019").Id, Quantity = 128 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000020").Id, Quantity = 123 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000021").Id, Quantity = 155 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000022").Id, Quantity = 78 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000023").Id, Quantity = 166 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000024").Id, Quantity = 117 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000025").Id, Quantity = 168 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000026").Id, Quantity = 197 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000027").Id, Quantity = 36 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000028").Id, Quantity = 145 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000029").Id, Quantity = 61 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000030").Id, Quantity = 139 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000031").Id, Quantity = 47 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000032").Id, Quantity = 154 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000033").Id, Quantity = 194 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000034").Id, Quantity = 41 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000035").Id, Quantity = 154 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000036").Id, Quantity = 71 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000037").Id, Quantity = 49 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000038").Id, Quantity = 165 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000039").Id, Quantity = 73 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000040").Id, Quantity = 176 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000041").Id, Quantity = 41 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000042").Id, Quantity = 34 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000043").Id, Quantity = 175 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000044").Id, Quantity = 59 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000045").Id, Quantity = 198 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000046").Id, Quantity = 106 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000047").Id, Quantity = 99 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000048").Id, Quantity = 55 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000049").Id, Quantity = 62 },
                new Inventory { ProductId = context.Product.First(p => p.Barcode == "8900000000050").Id, Quantity = 33 }
            };
            await context.Inventory.AddRangeAsync(inventory);
        }
    }
}