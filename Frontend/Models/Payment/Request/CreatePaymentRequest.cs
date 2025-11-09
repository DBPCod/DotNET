using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos.Requests.Payment;

public class CreatePaymentRequest
{
    [Required(ErrorMessage = "OrderId là bắt buộc")]
    public Guid OrderId { get; set; }

    [Required(ErrorMessage = "Amount là bắt buộc")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Số tiền phải lớn hơn 0")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "PaymentMethod là bắt buộc")]
    public string PaymentMethod { get; set; } = "cash";  // Mặc định 'cash', có thể là 'cash' hoặc 'card'

    // PaymentDate là optional, backend sẽ set default DateTime.Now nếu không cung cấp
    public DateTime? PaymentDate { get; set; }
}