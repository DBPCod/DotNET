using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("products")]
public class Product
{
    [Key]
    [Column("product_id")]
    public int ProductId { get; set; }

    [Column("category_id")]
    public int? CategoryId { get; set; }

    [Column("supplier_id")]
    public int? SupplierId { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("product_name")]
    public string ProductName { get; set; } = "";

    [MaxLength(50)]
    [Column("barcode")]
    public string? Barcode { get; set; }

    [Column("price", TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    [MaxLength(20)]
    [Column("unit")]
    public string Unit { get; set; } = "pcs";

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation properties
    public Category? Category { get; set; }
    public Supplier? Supplier { get; set; }
}
