using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("customers")]
public class Customer
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(20)]
    [Column("customer_id")]
    public string? CustomerId { get; set; } // CUS001, CUS002, ...

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

    [Required]
    [MaxLength(20)]
    [Column("status")]
    public string Status { get; set; } = "ACTIVE"; // ACTIVE hoặc PENDING

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
