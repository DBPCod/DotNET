using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class InventoryRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<Inventory> AddAsync(Inventory inventory)
    {
        inventory.UpdatedAt = DateTime.Now; // Cập nhật thời gian
        _context.Inventory.Add(inventory);
        await _context.SaveChangesAsync();
        return inventory;
    }
    public async Task<Inventory?> GetByProductIdAsync(Guid productId)
    {
        return await _context.Inventory.FirstOrDefaultAsync(i => i.ProductId == productId);
    }

    public async Task UpdateAsync(Inventory inventory)
    {
        _context.Inventory.Update(inventory);
        await _context.SaveChangesAsync();
    }
}
