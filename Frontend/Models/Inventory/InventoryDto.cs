namespace Frontend.Models.Inventory;

public class InventoryDto
{
    public string Id { get; set; } = "";
    public string ProductId { get; set; } = "";
    public int Quantity { get; set; }
    public decimal CostPrice { get; set; } = 0; // Giá nhập
    public DateTime UpdatedAt { get; set; }
    
    // Navigation properties (được join từ backend)
    public string? ProductName { get; set; }
    public string? ProductBarcode { get; set; }
    public decimal? ProductPrice { get; set; } // Giá bán
    public string? ProductUnit { get; set; }
    public string? CategoryName { get; set; }
    public string? SupplierName { get; set; }
    public string? ProductImagePath { get; set; }
    public bool? ProductStatus { get; set; }
    
    // Computed properties for UI
    public string StatusText => GetStatusText();
    public string StatusBadgeClass => GetStatusBadgeClass();
    public decimal? TotalCostValue => Quantity * CostPrice;
    public decimal? TotalSellValue => Quantity * (ProductPrice ?? 0);
    
    private string GetStatusText()
    {
        return Quantity > 0 ? "Còn hàng" : "Hết hàng";
    }
    
    private string GetStatusBadgeClass()
    {
        return Quantity > 0 ? "bg-success" : "bg-danger";
    }
}