namespace Backend.Utils.Mappers;

public static class UserMapper
{
    public static UserDto MapEntityToDto(User entity)
    {
        if (entity == null) return null!;

        return new UserDto
        {
            Id = entity.Id.ToString(),
            Email = entity.Email,
            Role = entity.Role.ToString(),
        };
    }

    public static List<UserDto> MapListEntityToListDto(IEnumerable<User> entities)
    {
        return [.. entities
                .Where(u => u != null)
                .Select(MapEntityToDto)];
    }

    public static User MapDtoToEntity(UserDto dto)
    {
        if (dto == null) return null!;

        return new User
        {
            Id = string.IsNullOrEmpty(dto.Id) ? Guid.NewGuid() : Guid.Parse(dto.Id),
            Email = dto.Email ?? null!,
            Role = Enum.TryParse<UserRole>(dto.Role, out var role) ? role : UserRole.STAFF,
        };
    }

    public static List<User> MapListDtoToListEntity(IEnumerable<UserDto> dtos)
    {
        return [.. dtos
                .Where(c => c != null)
                .Select(MapDtoToEntity)];
    }

}
