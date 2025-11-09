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

    [HttpGet]
    public async Task<IActionResult> GetAllPayments()
    {
        var payments = await _paymentService.GetAllPaymentsAsync();
        return Ok(payments);
    }

    [HttpGet("order/{orderId:guid}")]
    public async Task<IActionResult> GetPaymentsByOrderId(Guid orderId)
    {
        var response = new Response();

        try
        {
            var payments = await _paymentService.GetPaymentsByOrderIdAsync(orderId);

            if (!payments.Any())
            {
                response.Message = "No payments found for this order";
                response.StatusCode = 404;
                return StatusCode(response.StatusCode, response);
            }

            var paymentDtos = PaymentMapper.MapListEntityToListDto(payments);  // Giả sử có PaymentMapper tương tự OrderMapper

            response.Message = "Payments retrieved successfully";
            response.StatusCode = 200;
            response.Data.PaymentDtos = paymentDtos;  // Giả sử Data có property Payments (List<PaymentDto>)
        }
        catch (ExceptionCustom ex)
        {
            response.Message = ex.Message;
            response.StatusCode = ex.StatusCode;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.StatusCode = 500;
        }

        return StatusCode(response.StatusCode, response);
    }
}
