using Microsoft.EntityFrameworkCore;
using Backend.Contexts;
using Backend.Models;

namespace Backend.Repositories;

public class CategoryRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<List<Category>> GetAllAsync(int page, int pageSize, string? q = null)
    {
        var query = _context.Category.AsQueryable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.Trim();
            query = query.Where(c => c.CategoryName.Contains(term));
        }

        return await query
            .OrderBy(c => c.CategoryName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await _context.Category.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<bool> ExistsByNameAsync(string name, Guid? excludeId = null)
    {
        var query = _context.Category.AsQueryable();
        if (excludeId.HasValue)
            query = query.Where(c => c.Id != excludeId.Value);

        return await query.AnyAsync(c => c.CategoryName == name);
    }

    public async Task<bool> IsUsedByProductsAsync(Guid id)
    {
        return await _context.Product.AnyAsync(p => p.CategoryId == id);
    }

    public async Task AddAsync(Category category)
    {
        await _context.Category.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        _context.Category.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Category category)
    {
        _context.Category.Remove(category);
        await _context.SaveChangesAsync();
    }
}
