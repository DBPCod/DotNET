using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos.Requests;

public class UpdateInventoryRequest
{
    [Required]
    public Guid ProductId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
    public int Quantity { get; set; }
}
