using Microsoft.EntityFrameworkCore;
using Backend.Contexts;
using Backend.Models;

namespace Backend.Repositories;

public class SupplierRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<List<Supplier>> GetAllAsync(int page, int pageSize)
    {
        return await _context.Supplier
            .OrderBy(s => s.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Supplier?> GetByIdAsync(Guid id)
    {
        return await _context.Supplier.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task AddAsync(Supplier supplier)
    {
        await _context.Supplier.AddAsync(supplier);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Supplier supplier)
    {
        _context.Supplier.Update(supplier);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Supplier supplier)
    {
        _context.Supplier.Remove(supplier);
        await _context.SaveChangesAsync();
    }
}
