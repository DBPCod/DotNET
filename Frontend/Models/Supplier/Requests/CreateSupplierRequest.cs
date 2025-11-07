using System.ComponentModel.DataAnnotations;

namespace Frontend.Models.Supplier.Requests;

public class CreateSupplierRequest
{
    [Required(ErrorMessage = "Tên nhà cung cấp là bắt buộc")]
    [MaxLength(100, ErrorMessage = "Tên không được vượt quá 100 ký tự")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
    [MaxLength(20, ErrorMessage = "Số điện thoại không được vượt quá 20 ký tự")]
    [RegularExpression(@"^(\+84|0)[3-9]\d{8}$", ErrorMessage = "Số điện thoại không hợp lệ")]
    public string Phone { get; set; } = "";

    [Required(ErrorMessage = "Email là bắt buộc")]
    [MaxLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự")]
    [EmailAddress(ErrorMessage = "Định dạng email không hợp lệ")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Địa chỉ là bắt buộc")]
    [MaxLength(500, ErrorMessage = "Địa chỉ không được vượt quá 500 ký tự")]
    public string Address { get; set; } = "";

    public bool Status { get; set; } = true;
}