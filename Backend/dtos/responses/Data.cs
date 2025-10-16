namespace Backend.Dtos.Responses;

public class Data
{
    // User
    public UserDto? User { get; set; }
    public ICollection<UserDto>? Users { get; set; }
}