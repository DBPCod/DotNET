using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos.Requests;

public class CreateInventoryRequest
{
    [Required]
    public Guid ProductId { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative number")]
    public int Quantity { get; set; }
}
