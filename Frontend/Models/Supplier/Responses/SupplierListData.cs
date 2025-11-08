using Frontend.Models.Common;

namespace Frontend.Models.Supplier.Responses;

public class SupplierListData
{
    public List<SupplierDto> Suppliers { get; set; } = new();
    public PaginationInfo Pagination { get; set; } = new();
}