namespace Backend.Utils.Mappers;

public static class CategoryMapper
{
    public static CategoryDto MapEntityToDto(Category entity, int productCount = 0)
    {
        if (entity == null) return null!;

        return new CategoryDto
        {
            Id = entity.Id.ToString(),
            CategoryName = entity.CategoryName,
            Description = entity.Description,
            Status = ((int)entity.Status).ToString(),  // Convert to "0" or "1"
            ProductCount = productCount
        };
    }

    public static List<CategoryDto> MapListEntityToListDto(IEnumerable<Category> entities, Dictionary<Guid, int>? productCounts = null)
    {
        return [.. entities
                .Where(c => c != null)
                .Select(e => MapEntityToDto(e, productCounts?.GetValueOrDefault(e.Id, 0) ?? 0))];
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