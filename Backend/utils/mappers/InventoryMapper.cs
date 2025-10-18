namespace Backend.Utils.Mappers;

public static class InventoryMapper
{
    public static InventoryDto MapEntityToDto(Inventory entity)
    {
        if (entity == null) return null!;

        return new InventoryDto
        {
            Id = entity.Id.ToString(),
            ProductId = entity.ProductId.ToString(),
            Quantity = entity.Quantity,
            UpdatedAt = entity.UpdatedAt,
        };
    }

    public static List<InventoryDto> MapListEntityToListDto(IEnumerable<Inventory> entities)
    {
        return [.. entities
                .Where(i => i != null)
                .Select(MapEntityToDto)];
    }

    public static Inventory MapDtoToEntity(InventoryDto dto)
    {
        if (dto == null) return null!;

        return new Inventory
        {
            Id = string.IsNullOrEmpty(dto.Id) ? Guid.NewGuid() : Guid.Parse(dto.Id),
            ProductId = string.IsNullOrEmpty(dto.ProductId) ? Guid.NewGuid() : Guid.Parse(dto.ProductId),
            Quantity = dto.Quantity,
            UpdatedAt = dto.UpdatedAt,
        };
    }

    public static List<Inventory> MapListDtoToListEntity(IEnumerable<InventoryDto> dtos)
    {
        return [.. dtos
                .Where(i => i != null)
                .Select(MapDtoToEntity)];
    }
}