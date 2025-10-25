namespace Backend.Dtos.Responses;

public class Data
{
    // User
    public UserDto? User { get; set; }
    public ICollection<UserDto>? Users { get; set; }

    // Promotion
    public PromotionDto? Promotion { get; set; }
    public ICollection<PromotionDto>? Promotions { get; set; }

    // Promotion utilities
    public ValidatePromotionResponse? ValidationResult { get; set; }
    public ApplyPromoResponse? ApplyPromoResult { get; set; }

    // Pagination (áp dụng khi list nhiều item)
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
