namespace Backend.Utils.Mappers;

public static class OrderMapper
{
    public static OrderDto MapEntityToDto(Order entity)
    {
        if (entity == null) return null!;

        return new OrderDto
        {
            Id = entity.Id.ToString(),
            CustomerId = entity.CustomerId?.ToString(),
            UserId = entity.UserId?.ToString(),
            PromoId = entity.PromoId?.ToString(),
            OrderDate = entity.OrderDate,
            Status = entity.Status,
            TotalAmount = entity.TotalAmount,
            DiscountAmount = entity.DiscountAmount,
        };
    }

    public static List<OrderDto> MapListEntityToListDto(IEnumerable<Order> entities)
    {
        return [.. entities
                .Where(o => o != null)
                .Select(MapEntityToDto)];
    }

    public static Order MapDtoToEntity(OrderDto dto)
    {
        if (dto == null) return null!;

        return new Order
        {
            Id = string.IsNullOrEmpty(dto.Id) ? Guid.NewGuid() : Guid.Parse(dto.Id),
            CustomerId = string.IsNullOrEmpty(dto.CustomerId) ? null : Guid.Parse(dto.CustomerId),
            UserId = string.IsNullOrEmpty(dto.UserId) ? null : Guid.Parse(dto.UserId),
            PromoId = string.IsNullOrEmpty(dto.PromoId) ? null : Guid.Parse(dto.PromoId),
            OrderDate = dto.OrderDate,
            Status = dto.Status ?? null!,
            TotalAmount = dto.TotalAmount,
            DiscountAmount = dto.DiscountAmount,
        };
    }

    public static List<Order> MapListDtoToListEntity(IEnumerable<OrderDto> dtos)
    {
        return [.. dtos
                .Where(o => o != null)
                .Select(MapDtoToEntity)];
    }
}