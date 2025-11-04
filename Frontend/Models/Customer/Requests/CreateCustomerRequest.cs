using System.ComponentModel.DataAnnotations;

namespace Frontend.Models.Customer.Requests;

public class CreateCustomerRequest
{
    [Required(ErrorMessage = "Tên khách hàng là bắt buộc")]
    [MaxLength(100, ErrorMessage = "Tên không được vượt quá 100 ký tự")]
    public string Name { get; set; } = "";

    [MaxLength(20, ErrorMessage = "Số điện thoại không được vượt quá 20 ký tự")]
    public string? Phone { get; set; }

    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    [MaxLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự")]
    public string? Email { get; set; }

    public string? Address { get; set; }

    [MaxLength(20)]
    public string? Status { get; set; } = "ACTIVE";
}