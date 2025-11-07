using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class OrderRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<List<Order>> HandleGetAllOrder()
    {
        try
        {
            return await _context.Order.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<(List<Order> orders, int totalCount)> HandleGetOrdersWithPagination(
        int page, int pageSize, string? searchTerm = null, string? status = null)
    {
        try
        {
            var query = _context.Order
                .Include(o => o.Customer)  // Include Customer để load data cho search và display
                .AsQueryable();

            // Search by order ID, customer name, or customer email (tương tự search bằng username, email, full name ở User)
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(o =>
                    o.Id.ToString().Contains(searchTerm) ||
                    (!string.IsNullOrEmpty(o.Customer.Name) && o.Customer.Name.Contains(searchTerm)) ||
                    (!string.IsNullOrEmpty(o.Customer.Email) && o.Customer.Email.Contains(searchTerm)));
            }

            // Filter by status (tương tự status ở User, case-insensitive vì Status là string)
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(o => o.Status.ToLower() == status.ToLower());
            }

            var totalCount = await query.CountAsync();

            var orders = await query
                .OrderBy(o => o.OrderDate)  // Tương tự OrderBy CreatedAt ở User (ascending, dùng field có sẵn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (orders, totalCount);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Order?> HandleGetOrderById(Guid id)
    {
        try
        {
            return await _context.Order
                .FirstOrDefaultAsync(o => o.Id == id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Order> HandleCreateOrder(Order order)
    {
        try
        {
            await _context.Order.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Order> HandleUpdateOrder(Order order)
    {
        try
        {
            _context.Order.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> HandleUpdateStatus(Guid id, string newStatus)
    {
        try
        {
            var order = await _context.Order.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
                return false;

            order.Status = newStatus.ToLower();
            _context.Order.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> HandleSoftDeleteOrder(Guid id)
    {
        try
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
                return false;

            order.Status = "canceled";  // Tương tự soft delete bằng status ở User
            _context.Order.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}