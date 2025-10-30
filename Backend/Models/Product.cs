using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("products")]
public class Product
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("category_id")]
    public Guid? CategoryId { get; set; }

    [Column("supplier_id")]
    public Guid? SupplierId { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("product_name")]
    public string ProductName { get; set; } = "";

    [MaxLength(50)]
    [Column("barcode")]
    public string? Barcode { get; set; }

    [Column("price", TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    [Required]
    [MaxLength(20)]
    [Column("unit")]
    public string Unit { get; set; } = "pcs";

    // Đường dẫn lưu hình ảnh
    [MaxLength(500)]
    [Column("image_path")]
    public string? ImagePath { get; set; }

    // Trường Status cho Soft Delete (true = Active, false = Deleted)
    [Column("status")]
    public bool Status { get; set; } = true;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation properties
    public Category? Category { get; set; }
    public Supplier? Supplier { get; set; }
}