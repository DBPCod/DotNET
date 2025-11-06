namespace Backend.Utils.Mappers;

public static class CategoryMapper
{
    public static CategoryDto MapEntityToDto(Category entity)
    {
        if (entity == null) return null!;

        return new CategoryDto
        {
            Id = entity.Id.ToString(),
            CategoryName = entity.CategoryName,
            Description = entity.Description,
            Status = ((int)entity.Status).ToString(),  // Convert to "0" or "1"
        };
    }

    public static List<CategoryDto> MapListEntityToListDto(IEnumerable<Category> entities)
    {
        return [.. entities
                .Where(c => c != null)
                .Select(MapEntityToDto)];
    }

    public static Category MapDtoToEntity(CategoryDto dto)
    {
        if (dto == null) return null!;

        return new Category
        {
            Id = string.IsNullOrEmpty(dto.Id) ? Guid.NewGuid() : Guid.Parse(dto.Id),
            CategoryName = dto.CategoryName ?? null!,
            Status = Enum.TryParse<CategoryStatus>(dto.Status, out var status) 
                ? status 
                : CategoryStatus.Active,
        };
    }

    public static List<Category> MapListDtoToListEntity(IEnumerable<CategoryDto> dtos)
    {
        return [.. dtos
                .Where(c => c != null)
                .Select(MapDtoToEntity)];
    }
}