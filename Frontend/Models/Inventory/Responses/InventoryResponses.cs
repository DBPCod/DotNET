using Frontend.Models.Common;

namespace Frontend.Models.Inventory.Responses;

public class InventoryListResponse
{
    public List<InventoryDto> Inventories { get; set; } = new();
    public PaginationInfo Pagination { get; set; } = new();
}

public class InventoryDetailResponse
{
    public InventoryDto? Inventory { get; set; }
}

public class InventoryStatsResponse
{
    public int TotalItems { get; set; }
    public int InStockItems { get; set; }
    public int OutOfStockItems { get; set; }
    public decimal TotalInventoryValue { get; set; }
}