using Microsoft.EntityFrameworkCore;
using Backend.Contexts;
using Backend.Models;

namespace Backend.Repositories;

public class ProductRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    // Chỉ lấy sản phẩm chưa bị xóa (Status = true)
    public async Task<List<Product>> GetAllAsync(int page, int pageSize)
    {
        return await _context.Product
            .Where(p => p.Status == true)
            .OrderBy(p => p.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    // Lấy theo ID, chỉ lấy sản phẩm chưa bị xóa
    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Product
            .FirstOrDefaultAsync(p => p.Id == id && p.Status == true);
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

    // Soft Delete: Đổi Status = false thay vì xóa thật
    public async Task SoftDeleteAsync(Product product)
    {
        product.Status = false;
        _context.Product.Update(product);
        await _context.SaveChangesAsync();
    }

    // Hard Delete (nếu cần xóa thật)
    public async Task HardDeleteAsync(Product product)
    {
        _context.Product.Remove(product);
        await _context.SaveChangesAsync();
    }
}