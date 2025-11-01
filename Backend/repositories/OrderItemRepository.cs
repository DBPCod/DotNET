namespace Backend.Repositories;
using Microsoft.EntityFrameworkCore;

public class OrderItemRepository
{
    private readonly AppDbContext _context;

    public OrderItemRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<OrderItem>> AddOrderItemAsync(List<OrderItem> items)
    {
        _context.OrderItem.AddRange(items);
        await _context.SaveChangesAsync();
        return items;
    }

    public async Task<decimal> GetProductPriceAsync(Guid productId)
    {
        var product = await _context.Product.FirstOrDefaultAsync(p => p.Id == productId);
        if (product == null) throw new Exception($"Product {productId} not found");
        return product.Price;
    }

    public async Task UpdateOrderTotalAsync(Guid orderId, decimal totalAmount)
    {
        var order = await _context.Order.FirstOrDefaultAsync(o => o.Id == orderId);
        if (order == null) throw new Exception("Order not found");

        order.TotalAmount = totalAmount;
        _context.Order.Update(order);
        await _context.SaveChangesAsync();
    }
}
