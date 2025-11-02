using Backend.Dtos;
using Backend.Models;

namespace Backend.Services.Apis;

public class PaymentService(PaymentRepository paymentRepository)
{
    private readonly PaymentRepository _paymentRepository = paymentRepository;

    public async Task<Payment> CreatePaymentAsync(CreatePaymentRequest dto)
    {
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
}
