namespace Frontend.Models.User.Responses;

using Frontend.Models.Common;

/// <summary>
/// Response cho danh sách users có phân trang
/// </summary>
public class UserListResponse
{
    public List<UserDto> Users { get; set; } = new();
    public PaginationInfo Pagination { get; set; } = new();
}

