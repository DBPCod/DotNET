using Microsoft.EntityFrameworkCore;
using Backend.Contexts;
using Backend.Models;

namespace Backend.Repositories;

public class ProductRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<List<Product>> GetAllAsync(int page, int pageSize)
    {
        return await _context.Product
            .OrderBy(p => p.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Product.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Product product)
    {
        await _context.Product.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Product.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        _context.Product.Remove(product);
        await _context.SaveChangesAsync();
    }
}
