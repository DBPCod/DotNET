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
            _context.Order.Add(order);
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

    public async Task<bool> HandleDeleteOrder(Guid id)
    {
        try
        {
            var order = await _context.Order
                .FirstOrDefaultAsync(o => o.Id == id);
            if(order == null)
            {
                return false;
            }
            order.Status = "canceled";
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
