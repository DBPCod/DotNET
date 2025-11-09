using System;
using System.ComponentModel.DataAnnotations;

namespace Frontend.Models.Order
{
    public class CreateOrderRequest
    {
        [Required(ErrorMessage = "CustomerId is required")]
        public Guid CustomerId { get; set; }

        [Required(ErrorMessage = "UserId is required")]
        public Guid UserId { get; set; }

        public Guid? PromoId { get; set; }

        [Range(0, 999999999.99, ErrorMessage = "TotalAmount must be >= 0")]
        public decimal? TotalAmount { get; set; } = 0;

        [Range(0, 999999999.99, ErrorMessage = "DiscountAmount must be >= 0")]
        public decimal DiscountAmount { get; set; } = 0;
    }
}
