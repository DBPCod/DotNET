using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

[Table("users")]
[Index(nameof(Username), IsUnique = true)]
public class User
{
    [Key]
    [Column("user_id")]
    public int UserId { get; set; }

    [MaxLength(50)]
    [Required]
    [Column("username")]
    public string Username { get; set; } = "";

    [MaxLength(255)]
    [Required]
    [Column("password")]
    public string Password { get; set; } = "";

    [MaxLength(100)]
    [Column("full_name")]
    public string? FullName { get; set; }

    [Column("role")]
    public string Role { get; set; } = "staff"; // 'admin' or 'staff'

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation properties
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}