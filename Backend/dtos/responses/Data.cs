namespace Backend.Dtos.Responses;

public class Data
{
    // User
    public UserDto? User { get; set; }
    public ICollection<UserDto>? Users { get; set; }

    public PaginationInfo? Pagination { get; set; }
}

public class PaginationInfo
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
}