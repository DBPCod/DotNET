using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos.Requests;

public class CreatePaymentRequest
{
    [Required(ErrorMessage = "OrderId is required")]
    public Guid OrderId { get; set; }

    [Required(ErrorMessage = "Amount is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "PaymentMethod is required")]
    [RegularExpression("cash|card|bank_transfer|e-wallet", ErrorMessage = "Invalid payment method")]
    public string PaymentMethod { get; set; } = "cash";
}
