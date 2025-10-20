namespace Backend.Utils.Mappers;

public static class PromotionMapper
{
    public static PromotionDto MapEntityToDto(Promotion entity)
    {
        if (entity == null) return null!;

        return new PromotionDto
        {
            Id = entity.Id.ToString(),
            PromoCode = entity.PromoCode,
            Description = entity.Description,
            DiscountType = entity.DiscountType,
            DiscountValue = entity.DiscountValue,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            MinOrderAmount = entity.MinOrderAmount,
            UsageLimit = entity.UsageLimit,
            UsedCount = entity.UsedCount,
            Status = entity.Status,
        };
    }

    public static List<PromotionDto> MapListEntityToListDto(IEnumerable<Promotion> entities)
    {
        return [.. entities
                .Where(p => p != null)
                .Select(MapEntityToDto)];
    }

    public static Promotion MapDtoToEntity(PromotionDto dto)
    {
        if (dto == null) return null!;

        return new Promotion
        {
            Id = string.IsNullOrEmpty(dto.Id) ? Guid.NewGuid() : Guid.Parse(dto.Id),
            PromoCode = dto.PromoCode ?? null!,
            Description = dto.Description,
            DiscountType = dto.DiscountType ?? null!,
            DiscountValue = dto.DiscountValue,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            MinOrderAmount = dto.MinOrderAmount,
            UsageLimit = dto.UsageLimit,
            UsedCount = dto.UsedCount,
            Status = dto.Status ?? null!,
        };
    }

    public static List<Promotion> MapListDtoToListEntity(IEnumerable<PromotionDto> dtos)
    {
        return [.. dtos
                .Where(p => p != null)
                .Select(MapDtoToEntity)];
    }
}