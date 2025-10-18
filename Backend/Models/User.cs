using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

[Table("Users")]
[Index(nameof(Email), IsUnique = true)]
public class User
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(50)]
    [Required]
    [Column("username")]
    public string Username { get; set; } = "";

    [MaxLength(50)]
    [Required]
    [Column("email")]
    public string Email { get; set; } = "";

    [MaxLength(255)]
    [Required]
    [Column("password")]
    public string Password { get; set; } = "";

    [MaxLength(255)]
    [Column("full_name")]
    public string FullName { get; set; } = "";

    [Column("role")]
    public UserRole Role { get; set; } = UserRole.STAFF;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation properties
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}

public enum UserRole
{
    ADMIN,
    STAFF
}