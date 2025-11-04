using Microsoft.EntityFrameworkCore;
using Backend.Contexts;
using Backend.Models;

namespace Backend.Repositories;

public class CustomerRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;
    
    public async Task<List<Customer>> GetAllAsync(int page, int pageSize, string? search = null, string? status = null)
    {
        var query = _context.Customer.AsQueryable();

        // Tìm kiếm theo tên, email, phone, customerId
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(c => 
                c.Name.Contains(search) || 
                (c.Email != null && c.Email.Contains(search)) ||
                (c.Phone != null && c.Phone.Contains(search)) ||
                (c.CustomerId != null && c.CustomerId.Contains(search))
            );
        }

        // Lọc theo status
        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(c => c.Status == status);
        }

        return await query
            .OrderBy(c => c.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await _context.Customer.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Customer?> GetLastCustomerAsync()
    {
        return await _context.Customer
            .OrderByDescending(c => c.CustomerId)
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(Customer customer)
    {
        await _context.Customer.AddAsync(customer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Customer customer)
    {
        _context.Customer.Update(customer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Customer customer)
    {
        _context.Customer.Remove(customer);
        await _context.SaveChangesAsync();
    }
}
