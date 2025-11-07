using Microsoft.EntityFrameworkCore;
using Backend.Contexts;
using Backend.Models;

namespace Backend.Repositories;

public class ProductRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    // Get all with search, filter and pagination
    public async Task<(List<Product> products, int totalCount)> GetAllWithFilterAsync(
        int page, 
        int pageSize,
        string? searchQuery = null,
        Guid? categoryId = null,
        Guid? supplierId = null,
        bool? status = null)
    {
        var query = _context.Product
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .AsQueryable();

        // Search by product name or barcode
        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            var search = searchQuery.Trim().ToLower();
            query = query.Where(p => 
                p.ProductName.ToLower().Contains(search) || 
                (p.Barcode != null && p.Barcode.ToLower().Contains(search))
            );
        }

        // Filter by category
        if (categoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == categoryId.Value);
        }

        // Filter by supplier
        if (supplierId.HasValue)
        {
            query = query.Where(p => p.SupplierId == supplierId.Value);
        }

        // Filter by status
        if (status.HasValue)
        {
            query = query.Where(p => p.Status == status.Value);
        }
        // Nếu status = null thì hiển thị cả đang bán và ngừng bán (không filter)

        var totalCount = await query.CountAsync();

        var products = await query
            .OrderByDescending(p => p.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (products, totalCount);
    }

    // Chá»‰ láº¥y sáº£n pháº©m chÆ°a bá»‹ xÃ³a (Status = true)
    public async Task<List<Product>> GetAllAsync(int page, int pageSize)
    {
        return await _context.Product
            .Where(p => p.Status == true)
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .OrderByDescending(p => p.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    // Láº¥y theo ID, láº¥y cáº£ sáº£n pháº©m Ä'ang báº£n vÃ  ngÆ°á»ng báº£n
    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Product
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .FirstOrDefaultAsync(p => p.Id == id);
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

    // Soft Delete: Äá»•i Status = false thay vÃ¬ xÃ³a tháº­t
    public async Task SoftDeleteAsync(Product product)
    {
        product.Status = false;
        _context.Product.Update(product);
        await _context.SaveChangesAsync();
    }

    // Hard Delete (náº¿u cáº§n xÃ³a tháº­t)
    public async Task HardDeleteAsync(Product product)
    {
        _context.Product.Remove(product);
        await _context.SaveChangesAsync();
    }

    // Toggle Status: Chuyển đổi giữa đang bán và ngừng bán
    public async Task ToggleStatusAsync(Product product)
    {
        product.Status = !product.Status;
        _context.Product.Update(product);
        await _context.SaveChangesAsync();
    }
}