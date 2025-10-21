using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos.Requests.User;

public class GetUsersRequest
{
    [Range(1, int.MaxValue, ErrorMessage = "Page must be greater than 0")]
    public int Page { get; set; } = 1;

    [Range(1, 100, ErrorMessage = "PageSize must be between 1 and 100")]
    public int PageSize { get; set; } = 10;

    public string? SearchTerm { get; set; }

    [RegularExpression("^(ADMIN|STAFF)$", ErrorMessage = "Role must be either ADMIN or STAFF")]
    public string? Role { get; set; }

    [RegularExpression("^(Username|Email|FullName|CreatedAt)$", ErrorMessage = "SortBy must be Username, Email, FullName, or CreatedAt")]
    public string SortBy { get; set; } = "CreatedAt";

    [RegularExpression("^(ASC|DESC)$", ErrorMessage = "SortOrder must be ASC or DESC")]
    public string SortOrder { get; set; } = "ASC";
}