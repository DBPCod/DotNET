using Backend.Dtos;
using Backend.Services.Apis;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/v1/payments")]
[ApiController]
public class PaymentController(PaymentService paymentService) : ControllerBase
{
    private readonly PaymentService _paymentService = paymentService;

    [HttpPost]
    public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequest dto)
    {
        try
        {
            var payment = await _paymentService.CreatePaymentAsync(dto);

            return Ok(new
            {
                message = "Payment created and order updated successfully",
                payment
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
