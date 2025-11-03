using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("categories")]
public class Category
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    [Column("category_name")]
    public string CategoryName { get; set; } = "";

    [MaxLength(500)]
    [Column("description", Order = 2)]
    public string Description { get; set; } = "";

    [Column("status", Order = 3)]
    public CategoryStatus Status { get; set; } = CategoryStatus.Active;
}

public enum CategoryStatus
{
    Deleted = 0,    // Tạm dừng (0)
    Active = 1      // Đang hoạt động (1)
}