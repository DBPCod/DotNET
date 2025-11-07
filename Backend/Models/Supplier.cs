using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("suppliers")]
public class Supplier
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    [Column("name")]
    public string Name { get; set; } = "";

    [Required]
    [MaxLength(20)]
    [Column("phone")]
    public string Phone { get; set; } = "";

    [Required]
    [MaxLength(100)]
    [Column("email")]
    public string Email { get; set; } = "";

    [Required]
    [MaxLength(500)]
    [Column("address")]
    public string Address { get; set; } = "";

    // Thêm trường Status cho Soft Delete
    [Column("status")]
    public bool Status { get; set; } = true; // true = Active, false = Inactive
}