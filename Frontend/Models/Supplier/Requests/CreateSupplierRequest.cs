namespace Frontend.Models.Supplier.Requests;

public class CreateSupplierRequest
{
    public string Name { get; set; } = "";
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public bool Status { get; set; } = true;
}