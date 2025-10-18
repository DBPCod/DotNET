using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("payments")]
public class Payment
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [Column("order_id")]
    public Guid OrderId { get; set; }

    [Required]
    [Column("amount", TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }

    [Column("payment_method")]
    public string PaymentMethod { get; set; } = "cash"; // 'cash', 'card', 'bank_transfer', 'e-wallet'

    [Column("payment_date")]
    public DateTime PaymentDate { get; set; } = DateTime.Now;

    // Navigation property
    public Order? Order { get; set; }
}
