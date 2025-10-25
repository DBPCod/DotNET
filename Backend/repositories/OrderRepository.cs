using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class OrderRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

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
}
