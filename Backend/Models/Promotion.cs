using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("promotions")]
public class Promotion
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(50)]
    [Column("promo_code")]
    public string PromoCode { get; set; } = "";

    [MaxLength(255)]
    [Column("description")]
    public string? Description { get; set; }

    [Required]
    [MaxLength(20)]
    [Column("discount_type")]
    public DiscountType DiscountType { get; set; } = DiscountType.Percent; // percent|fixed|free_shipping

    [Column("discount_value", TypeName = "decimal(10,2)")]
    public decimal DiscountValue { get; set; } = 0;

    [MaxLength(20)]
    [Column("promotion_type")]
    public PromotionType PromotionType { get; set; } = PromotionType.Promotion; // promotion|discount_code

    [Column("start_date", TypeName = "date")]
    public DateTime StartDate { get; set; } = DateTime.Today;

    [Column("end_date", TypeName = "date")]
    public DateTime EndDate { get; set; } = DateTime.Today.AddDays(30);

    [Column("min_order_amount", TypeName = "decimal(10,2)")]
    public decimal MinOrderAmount { get; set; } = 0;

    [Column("usage_limit")]
    public int UsageLimit { get; set; } = 0;

    [Column("used_count")]
    public int UsedCount { get; set; } = 0;

    [MaxLength(20)]
    [Column("status")]
    public PromotionStatus Status { get; set; } = PromotionStatus.Active; // active|inactive
}
