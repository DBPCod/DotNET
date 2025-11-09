using Backend.Dtos;
using Backend.Models;

namespace Backend.Services.Apis;

public class PaymentService(PaymentRepository paymentRepository)
{
    private readonly PaymentRepository _paymentRepository = paymentRepository;
    private readonly List<string> ValidPaymentMethods =
    [
        "cash",       
        "card",        
        "bank_transfer", 
        "e-wallet"     
    ];
    public async Task<Payment> CreatePaymentAsync(CreatePaymentRequest dto)
    {

        if (!ValidPaymentMethods.Contains(dto.PaymentMethod.ToLower()))
        {
            throw new ArgumentException(
                $"Invalid payment method. Allowed methods are: {string.Join(", ", ValidPaymentMethods)}"
            );
        }
        
        var payment = new Payment
        {
            OrderId = dto.OrderId,
            Amount = dto.Amount,
            PaymentMethod = dto.PaymentMethod,
            PaymentDate = DateTime.Now
        };

        // Thêm payment
        var addedPayment = await _paymentRepository.AddPaymentAsync(payment);

        // Cập nhật trạng thái Order sang "paid"
        await _paymentRepository.UpdateOrderStatusAsync(payment.OrderId, "paid");

        return addedPayment;
    }

    public async Task<List<Payment>> GetAllPaymentsAsync()
    {
        return await _paymentRepository.GetAllPaymentsAsync();
    }

    public async Task<List<Payment>> GetPaymentsByOrderIdAsync(Guid orderId)
    {
        return await _paymentRepository.GetPaymentsByOrderIdAsync(orderId);
    }
}
