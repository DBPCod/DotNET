namespace Backend.Utils.Mappers;

public static class SupplierMapper
{
    public static SupplierDto MapEntityToDto(Supplier entity)
    {
        if (entity == null) return null!;

        return new SupplierDto
        {
            Id = entity.Id.ToString(),
            Name = entity.Name,
            Phone = entity.Phone,
            Email = entity.Email,
            Address = entity.Address,
        };
    }

    public static List<SupplierDto> MapListEntityToListDto(IEnumerable<Supplier> entities)
    {
        return [.. entities
                .Where(s => s != null)
                .Select(MapEntityToDto)];
    }

    public static Supplier MapDtoToEntity(SupplierDto dto)
    {
        if (dto == null) return null!;

        return new Supplier
        {
            Id = string.IsNullOrEmpty(dto.Id) ? Guid.NewGuid() : Guid.Parse(dto.Id),
            Name = dto.Name ?? null!,
            Phone = dto.Phone,
            Email = dto.Email,
            Address = dto.Address,
        };
    }

    public static List<Supplier> MapListDtoToListEntity(IEnumerable<SupplierDto> dtos)
    {
        return [.. dtos
                .Where(s => s != null)
                .Select(MapDtoToEntity)];
    }
}