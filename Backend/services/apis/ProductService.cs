using Backend.Dtos;
using Backend.Dtos.Requests;
using Backend.Dtos.Responses;
using Backend.Models;
using Backend.Repositories;

namespace Backend.Services.Apis;

public class ProductService(ProductRepository productRepository)
{
    private readonly ProductRepository _productRepository = productRepository;

    public async Task<Response> GetAllAsync(int page, int pageSize)
    {
        var list = await _productRepository.GetAllAsync(page, pageSize);
        var response = new Response { StatusCode = 200, Message = "OK" };
        response.Data.Products = list.Select(p => new ProductDto
        {
            Id = p.Id.ToString(),
            CategoryId = p.CategoryId?.ToString(),
            SupplierId = p.SupplierId?.ToString(),
            ProductName = p.ProductName,
            Barcode = p.Barcode,
            Price = p.Price,
            Unit = p.Unit,
            CreatedAt = p.CreatedAt
        }).ToList();
        return response;
    }

    public async Task<Response> GetByIdAsync(Guid id)
    {
        var entity = await _productRepository.GetByIdAsync(id);
        var response = new Response();
        if (entity == null)
        {
            response.StatusCode = 404;
            response.Message = "Product not found";
            return response;
        }

        response.StatusCode = 200;
        response.Message = "OK";
        response.Data.Product = new ProductDto
        {
            Id = entity.Id.ToString(),
            CategoryId = entity.CategoryId?.ToString(),
            SupplierId = entity.SupplierId?.ToString(),
            ProductName = entity.ProductName,
            Barcode = entity.Barcode,
            Price = entity.Price,
            Unit = entity.Unit,
            CreatedAt = entity.CreatedAt
        };
        return response;
    }

    public async Task<Response> CreateAsync(CreateProductRequest req)
    {
        var entity = new Product
        {
            ProductName = req.ProductName,
            Barcode = req.Barcode,
            Price = req.Price,
            Unit = req.Unit,
        };

        if (Guid.TryParse(req.CategoryId, out var categoryId))
            entity.CategoryId = categoryId;
        if (Guid.TryParse(req.SupplierId, out var supplierId))
            entity.SupplierId = supplierId;

        await _productRepository.AddAsync(entity);

        var response = new Response { StatusCode = 201, Message = "Created" };
        response.Data.Product = new ProductDto
        {
            Id = entity.Id.ToString(),
            CategoryId = entity.CategoryId?.ToString(),
            SupplierId = entity.SupplierId?.ToString(),
            ProductName = entity.ProductName,
            Barcode = entity.Barcode,
            Price = entity.Price,
            Unit = entity.Unit,
            CreatedAt = entity.CreatedAt
        };
        return response;
    }

    public async Task<Response> UpdateAsync(Guid id, UpdateProductRequest req)
    {
        var entity = await _productRepository.GetByIdAsync(id);
        var response = new Response();
        if (entity == null)
        {
            response.StatusCode = 404;
            response.Message = "Product not found";
            return response;
        }

        entity.ProductName = req.ProductName;
        entity.Barcode = req.Barcode;
        entity.Price = req.Price;
        entity.Unit = req.Unit;
        entity.CategoryId = Guid.TryParse(req.CategoryId, out var categoryId) ? categoryId : null;
        entity.SupplierId = Guid.TryParse(req.SupplierId, out var supplierId) ? supplierId : null;

        await _productRepository.UpdateAsync(entity);

        response.StatusCode = 200;
        response.Message = "Updated";
        response.Data.Product = new ProductDto
        {
            Id = entity.Id.ToString(),
            CategoryId = entity.CategoryId?.ToString(),
            SupplierId = entity.SupplierId?.ToString(),
            ProductName = entity.ProductName,
            Barcode = entity.Barcode,
            Price = entity.Price,
            Unit = entity.Unit,
            CreatedAt = entity.CreatedAt
        };
        return response;
    }

    public async Task<Response> DeleteAsync(Guid id)
    {
        var entity = await _productRepository.GetByIdAsync(id);
        var response = new Response();
        if (entity == null)
        {
            response.StatusCode = 404;
            response.Message = "Product not found";
            return response;
        }

        await _productRepository.DeleteAsync(entity);
        response.StatusCode = 200;
        response.Message = "Deleted";
        return response;
    }
}