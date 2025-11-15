using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class InventoryRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<Inventory> AddAsync(Inventory inventory)
    {
        inventory.UpdatedAt = DateTime.Now;
        _context.Inventory.Add(inventory);
        await _context.SaveChangesAsync();
        return inventory;
    }

    public async Task<Inventory?> GetByIdAsync(Guid id)
    {
        return await _context.Inventory
            .Include(i => i.Product)
                .ThenInclude(p => p.Category)
            .Include(i => i.Product)
                .ThenInclude(p => p.Supplier)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Inventory?> GetByProductIdAsync(Guid productId)
    {
        return await _context.Inventory
            .Include(i => i.Product)
                .ThenInclude(p => p.Category)
            .Include(i => i.Product)
                .ThenInclude(p => p.Supplier)
            .FirstOrDefaultAsync(i => i.ProductId == productId);
    }

    public async Task<(List<Inventory> Items, int TotalCount)> GetAllAsync(
        int page = 1, 
        int pageSize = 10,
        string? searchQuery = null,
        string? status = null,
        Guid? categoryId = null,
        int? minQuantity = null)
    {
        var query = _context.Inventory
            .Include(i => i.Product)
                .ThenInclude(p => p.Category)
            .Include(i => i.Product)
                .ThenInclude(p => p.Supplier)
            .AsQueryable();

        // Search by product name or barcode
        if (!string.IsNullOrEmpty(searchQuery))
        {
            query = query.Where(i => 
                i.Product != null && i.Product.ProductName.Contains(searchQuery) ||
                (i.Product != null && i.Product.Barcode != null && i.Product.Barcode.Contains(searchQuery)));
        }

        // Filter by status
        if (!string.IsNullOrEmpty(status))
        {
            switch (status.ToLower())
            {
                case "in_stock":
                    query = query.Where(i => i.Quantity > 0);
                    break;
                case "out_of_stock":
                    query = query.Where(i => i.Quantity == 0);
                    break;
            }
        }

        // Filter by category
        if (categoryId.HasValue)
        {
            query = query.Where(i => i.Product!.CategoryId == categoryId);
        }

        // Filter by minimum quantity
        if (minQuantity.HasValue)
        {
            query = query.Where(i => i.Quantity >= minQuantity.Value);
        }

        var totalCount = await query.CountAsync();
        
        var items = await query
            .OrderByDescending(i => i.UpdatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }

    public async Task UpdateAsync(Inventory inventory)
    {
        inventory.UpdatedAt = DateTime.Now;
        _context.Inventory.Update(inventory);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Inventory inventory)
    {
        _context.Inventory.Remove(inventory);
        await _context.SaveChangesAsync();
    }

    public async Task<(int InStock, int OutOfStock, decimal TotalValue)> GetStatsAsync()
    {
        var inventories = await _context.Inventory
            .Include(i => i.Product)
            .ToListAsync();

        var inStock = inventories.Count(i => i.Quantity > 0);
        var outOfStock = inventories.Count(i => i.Quantity == 0);
        var totalValue = inventories.Sum(i => i.CostPrice * i.Quantity);

        return (inStock, outOfStock, totalValue);
    }
}
