using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("customers")]
public class Customer
{
    [Key]
    [Column("customer_id")]
    public int CustomerId { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("name")]
    public string Name { get; set; } = "";

    [MaxLength(20)]
    [Column("phone")]
    public string? Phone { get; set; }

    [MaxLength(100)]
    [Column("email")]
    public string? Email { get; set; }

    [Column("address")]
    public string? Address { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
