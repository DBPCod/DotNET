using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("categories")]
public class Category
{
    [Key]
    [Column("category_id")]
    public int CategoryId { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("category_name")]
    public string CategoryName { get; set; } = "";
}
