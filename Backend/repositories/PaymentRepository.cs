using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class PaymentRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<Payment> AddPaymentAsync(Payment payment)
    {
        _context.Payment.Add(payment);
        await _context.SaveChangesAsync();
        return payment;
    }

    public async Task UpdateOrderStatusAsync(Guid orderId, string status)
    {
        var order = await _context.Order.FirstOrDefaultAsync(o => o.Id == orderId);
        if (order == null) throw new Exception("Order not found");  

        order.Status = status;
        _context.Order.Update(order);
        await _context.SaveChangesAsync();
    }
}
