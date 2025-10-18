using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/v1/payments")]
[ApiController]
public class PaymentController(PaymentService paymentService) : ControllerBase
{
    private readonly PaymentService _paymentService = paymentService;

}