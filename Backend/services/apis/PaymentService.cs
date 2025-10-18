namespace Backend.Services.Apis;

public class PaymentService(PaymentRepository paymentRepository)
{
    private readonly PaymentRepository _paymentRepository = paymentRepository;

}
