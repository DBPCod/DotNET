namespace Backend.Utils.Mappers;

public static class CustomerMapper
{
    public static CustomerDto MapEntityToDto(Customer entity)
    {
        if (entity == null) return null!;

        return new CustomerDto
        {
            Id = entity.Id.ToString(),
            Name = entity.Name,
            Phone = entity.Phone,
            Email = entity.Email,
            Address = entity.Address,
            CreatedAt = entity.CreatedAt,
        };
    }

    public static List<CustomerDto> MapListEntityToListDto(IEnumerable<Customer> entities)
    {
        return [.. entities
                .Where(c => c != null)
                .Select(MapEntityToDto)];
    }

    public static Customer MapDtoToEntity(CustomerDto dto)
    {
        if (dto == null) return null!;

        return new Customer
        {
            Id = string.IsNullOrEmpty(dto.Id) ? Guid.NewGuid() : Guid.Parse(dto.Id),
            Name = dto.Name ?? null!,
            Phone = dto.Phone,
            Email = dto.Email,
            Address = dto.Address,
            CreatedAt = dto.CreatedAt,
        };
    }

    public static List<Customer> MapListDtoToListEntity(IEnumerable<CustomerDto> dtos)
    {
        return [.. dtos
                .Where(c => c != null)
                .Select(MapDtoToEntity)];
    }
}