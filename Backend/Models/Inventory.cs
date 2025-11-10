using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("inventory")]
public class Inventory
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [Column("product_id")]
    public Guid ProductId { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; } = 0;

    [Column("cost_price", TypeName = "decimal(10,2)")]
    public decimal CostPrice { get; set; } = 0; // Giá nhập

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // Navigation property
    public Product? Product { get; set; }
}
