namespace Backend.Utils.Mappers;

public static class OrderItemMapper
{
    public static OrderItemDto MapEntityToDto(OrderItem entity)
    {
        if (entity == null) return null!;

        return new OrderItemDto
        {
            Id = entity.Id.ToString(),
            OrderId = entity.OrderId.ToString(),
            ProductId = entity.ProductId.ToString(),
            Quantity = entity.Quantity,
            Price = entity.Price,
            Subtotal = entity.Subtotal,
        };
    }

    public static List<OrderItemDto> MapListEntityToListDto(IEnumerable<OrderItem> entities)
    {
        return [.. entities
                .Where(oi => oi != null)
                .Select(MapEntityToDto)];
    }

    public static OrderItem MapDtoToEntity(OrderItemDto dto)
    {
        if (dto == null) return null!;

        return new OrderItem
        {
            Id = string.IsNullOrEmpty(dto.Id) ? Guid.NewGuid() : Guid.Parse(dto.Id),
            OrderId = string.IsNullOrEmpty(dto.OrderId) ? Guid.NewGuid() : Guid.Parse(dto.OrderId),
            ProductId = string.IsNullOrEmpty(dto.ProductId) ? Guid.NewGuid() : Guid.Parse(dto.ProductId),
            Quantity = dto.Quantity,
            Price = dto.Price,
            Subtotal = dto.Subtotal,
        };
    }

    public static List<OrderItem> MapListDtoToListEntity(IEnumerable<OrderItemDto> dtos)
    {
        return [.. dtos
                .Where(oi => oi != null)
                .Select(MapDtoToEntity)];
    }
}