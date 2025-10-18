namespace Backend.Utils.Mappers;

public static class ProductMapper
{
    public static ProductDto MapEntityToDto(Product entity)
    {
        if (entity == null) return null!;

        return new ProductDto
        {
            Id = entity.Id.ToString(),
            CategoryId = entity.CategoryId?.ToString(),
            SupplierId = entity.SupplierId?.ToString(),
            ProductName = entity.ProductName,
            Barcode = entity.Barcode,
            Price = entity.Price,
            Unit = entity.Unit,
            CreatedAt = entity.CreatedAt,
        };
    }

    public static List<ProductDto> MapListEntityToListDto(IEnumerable<Product> entities)
    {
        return [.. entities
                .Where(p => p != null)
                .Select(MapEntityToDto)];
    }

    public static Product MapDtoToEntity(ProductDto dto)
    {
        if (dto == null) return null!;

        return new Product
        {
            Id = string.IsNullOrEmpty(dto.Id) ? Guid.NewGuid() : Guid.Parse(dto.Id),
            CategoryId = string.IsNullOrEmpty(dto.CategoryId) ? null : Guid.Parse(dto.CategoryId),
            SupplierId = string.IsNullOrEmpty(dto.SupplierId) ? null : Guid.Parse(dto.SupplierId),
            ProductName = dto.ProductName ?? null!,
            Barcode = dto.Barcode,
            Price = dto.Price,
            Unit = dto.Unit ?? null!,
            CreatedAt = dto.CreatedAt,
        };
    }

    public static List<Product> MapListDtoToListEntity(IEnumerable<ProductDto> dtos)
    {
        return [.. dtos
                .Where(p => p != null)
                .Select(MapDtoToEntity)];
    }
}