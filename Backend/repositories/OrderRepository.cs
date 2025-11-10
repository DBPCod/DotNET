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
    int page, int pageSize, string? searchTerm = null, string? status = null,
    DateTime? fromDate = null,
    DateTime? toDate = null
    )
    {
        try
        {
            // Thêm log params vào repo
            Console.WriteLine($"Repo Called - Search: {searchTerm}, Status: {status}");
            Console.WriteLine($"Repo FromDate: {fromDate?.Date}, ToDate: {toDate?.Date}");

            var query = _context.Order
                .Include(o => o.Customer)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(o =>
                    o.Id.ToString().Contains(searchTerm) ||
                    (!string.IsNullOrEmpty(o.Customer.Name) && o.Customer.Name.Contains(searchTerm)) ||
                    (!string.IsNullOrEmpty(o.Customer.Email) && o.Customer.Email.Contains(searchTerm)));
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(o => o.Status.ToLower() == status.ToLower());
            }

            // Log trước filter ngày
            Console.WriteLine("Before date filter - Query count: " + await query.CountAsync());

            if (fromDate.HasValue)
            {
                var from = fromDate.Value.Date;
                Console.WriteLine($"Applying fromDate filter: >= {from}");
                query = query.Where(o => o.OrderDate.Date >= from);
            }

            if (toDate.HasValue)
            {
                var to = toDate.Value.Date.AddDays(1).AddTicks(-1);
                Console.WriteLine($"Applying toDate filter: <= {to}");
                query = query.Where(o => o.OrderDate <= to);
            }

            var totalCount = await query.CountAsync();
            Console.WriteLine("After date filter - Total count: " + totalCount);

            var orders = await query
                .OrderBy(o => o.OrderDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            Console.WriteLine($"Final orders count: {orders.Count}");
            if (orders.Any())
            {
                Console.WriteLine($"Sample filtered OrderDate: {orders.First().OrderDate} (Date: {orders.First().OrderDate.Date})");
            }

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