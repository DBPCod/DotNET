using Microsoft.EntityFrameworkCore;
using Backend.Contexts;
using Backend.Models;

namespace Backend.Repositories;

public class SupplierRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    // Lấy tất cả suppliers (bao gồm cả inactive) - dùng cho admin xem
    public async Task<(List<Supplier> suppliers, int totalCount)> GetAllAsync(int page, int pageSize)
    {
        var query = _context.Supplier.AsQueryable();
        
        var totalCount = await query.CountAsync();
        var suppliers = await query
            .OrderBy(s => s.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
            
        return (suppliers, totalCount);
    }

    // Lấy chỉ các suppliers đang hoạt động - dùng cho dropdown
    public async Task<List<Supplier>> GetActiveAsync()
    {
        return await _context.Supplier
            .Where(s => s.Status == true)
            .OrderBy(s => s.Name)
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

    // Soft Delete
    public async Task SoftDeleteAsync(Supplier supplier)
    {
        supplier.Status = false;
        _context.Supplier.Update(supplier);
        await _context.SaveChangesAsync();
    }

    // Hard Delete (nếu cần)
    public async Task HardDeleteAsync(Supplier supplier)
    {
        _context.Supplier.Remove(supplier);
        await _context.SaveChangesAsync();
    }
}