namespace Frontend.Models;

public class PromotionDto
{
    public string Id { get; set; } = "";
    public string PromoCode { get; set; } = "";
    public string? Description { get; set; }
    public string DiscountType { get; set; } = "";
    public decimal DiscountValue { get; set; }
    public string PromotionType { get; set; } = "promotion";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal MinOrderAmount { get; set; }
    public int UsageLimit { get; set; }
    public int UsedCount { get; set; }
    public string Status { get; set; } = "";
    public bool CanEdit { get; set; } = true;
}

public class ApiResponse
{
    public string Message { get; set; } = "";
    public int StatusCode { get; set; }
    public ResponseData Data { get; set; } = new ResponseData();
}

public class ResponseData
{
    public List<PromotionDto>? Promotions { get; set; }
    public PromotionDto? Promotion { get; set; }
    public PaginationInfo? Pagination { get; set; }
}

public class PaginationInfo
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
}

public class CreatePromotionRequest
{
    public string PromoCode { get; set; } = "";
    public string? Description { get; set; }
    public string DiscountType { get; set; } = "";
    public decimal DiscountValue { get; set; } = 0;
    public string PromotionType { get; set; } = "promotion";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal MinOrderAmount { get; set; } = 0;
    public int UsageLimit { get; set; } = 0;
    public string Status { get; set; } = "active";
}

public class UpdatePromotionRequest
{
    public string? Description { get; set; }
    public string? DiscountType { get; set; }
    public decimal? DiscountValue { get; set; }
    public string? PromotionType { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? MinOrderAmount { get; set; }
    public int? UsageLimit { get; set; }
    public string? Status { get; set; }
}

