using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("orders")]
public class Order
{
    [Key]
    [Column("order_id")]
    public int OrderId { get; set; }

    [Column("customer_id")]
    public int? CustomerId { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("promo_id")]
    public int? PromoId { get; set; }

    [Column("order_date")]
    public DateTime OrderDate { get; set; } = DateTime.Now;

    [Column("status")]
    public string Status { get; set; } = "pending"; // 'pending', 'paid', 'canceled'

    [Column("total_amount", TypeName = "decimal(10,2)")]
    public decimal? TotalAmount { get; set; }

    [Column("discount_amount", TypeName = "decimal(10,2)")]
    public decimal DiscountAmount { get; set; } = 0;

    // Navigation properties
    public Customer? Customer { get; set; }
    public User? User { get; set; }
    public Promotion? Promotion { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
