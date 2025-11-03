namespace Frontend.Models.Promotions.Responses;

using Frontend.Models.Common;

/// <summary>
/// Data structure cho danh sách promotions từ backend
/// </summary>
public class PromotionListData
{
    public List<PromotionDto> Promotions { get; set; } = new();
    public PaginationInfo Pagination { get; set; } = new();
}

