namespace Backend.Utils.Mappers;

public static class PaymentMapper
{
    public static PaymentDto MapEntityToDto(Payment entity)
    {
        if (entity == null) return null!;

        return new PaymentDto
        {
            Id = entity.Id.ToString(),
            OrderId = entity.OrderId.ToString(),
            Amount = entity.Amount,
            PaymentMethod = entity.PaymentMethod,
            PaymentDate = entity.PaymentDate,
        };
    }

    public static List<PaymentDto> MapListEntityToListDto(IEnumerable<Payment> entities)
    {
        return [.. entities
                .Where(p => p != null)
                .Select(MapEntityToDto)];
    }

    public static Payment MapDtoToEntity(PaymentDto dto)
    {
        if (dto == null) return null!;

        return new Payment
        {
            Id = string.IsNullOrEmpty(dto.Id) ? Guid.NewGuid() : Guid.Parse(dto.Id),
            OrderId = string.IsNullOrEmpty(dto.OrderId) ? Guid.NewGuid() : Guid.Parse(dto.OrderId),
            Amount = dto.Amount,
            PaymentMethod = dto.PaymentMethod ?? null!,
            PaymentDate = dto.PaymentDate,
        };
    }

    public static List<Payment> MapListDtoToListEntity(IEnumerable<PaymentDto> dtos)
    {
        return [.. dtos
                .Where(p => p != null)
                .Select(MapDtoToEntity)];
    }
}