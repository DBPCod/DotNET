using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

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

    public async Task<List<OrderItem>> GetAllOrderItemsAsync()
    {
        return await _context.OrderItem.ToListAsync();
    }

    // GET - Order items theo orderId với phân trang (tương tự HandleGetOrderItemsWithPagination, không search)
    public async Task<(List<OrderItem> orderItems, int totalCount)> HandleGetOrderItemsByOrderIdWithPagination(
        Guid orderId, int page, int pageSize)
    {
        try
        {
            var query = _context.OrderItem
                .Include(oi => oi.Order)  // Include Order để load data nếu cần
                .Include(oi => oi.Product)  // Include Product để display nếu cần
                .Where(oi => oi.OrderId == orderId)  // Filter theo orderId
                .AsQueryable();

            var totalCount = await query.CountAsync();

            var orderItems = await query
                .OrderBy(oi => oi.Id)  // OrderBy theo ID (có thể thay bằng CreatedAt nếu có field đó)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (orderItems, totalCount);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    // GET - Order items với phân trang và tìm kiếm (tương tự HandleGetOrdersWithPagination ở OrderRepository)
    public async Task<(List<OrderItem> orderItems, int totalCount)> HandleGetOrderItemsWithPagination(
        int page, int pageSize)
    {
        try
        {
            var query = _context.OrderItem
                .Include(oi => oi.Order)  // Include Order để load data cho search/display nếu cần
                .Include(oi => oi.Product)  // Include Product để search theo tên sản phẩm
                .AsQueryable();

            var totalCount = await query.CountAsync();

            var orderItems = await query
                .OrderBy(oi => oi.Id)  // OrderBy theo ID (có thể thay bằng CreatedAt nếu có field đó)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (orderItems, totalCount);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}