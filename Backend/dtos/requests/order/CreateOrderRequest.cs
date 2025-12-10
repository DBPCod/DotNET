using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos.Requests.Order
{
    public class CreateOrderRequest
    {
        // CustomerId có thể null - nếu null sẽ tự động tìm/tạo từ User email
        public Guid? CustomerId { get; set; }

        [Required(ErrorMessage = "UserId is required")]
        public Guid UserId { get; set; }

        public Guid? PromoId { get; set; }

        [Range(0, 999999999.99, ErrorMessage = "TotalAmount must be >= 0")]
        public decimal? TotalAmount { get; set; } = 0;

        [Range(0, 999999999.99, ErrorMessage = "DiscountAmount must be >= 0")]
        public decimal DiscountAmount { get; set; } = 0;

        // Thông tin Customer để tạo mới (nếu CustomerId null)
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerAddress { get; set; }
    }
}
