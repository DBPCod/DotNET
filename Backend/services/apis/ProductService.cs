using Backend.Dtos;
using Backend.Dtos.Requests;
using Backend.Dtos.Responses;
using Backend.Models;
using Backend.Repositories;
using Backend.Services;
using Backend.Utils.Mappers;

namespace Backend.Services.Apis;

public class ProductService(ProductRepository productRepository, FileUploadService fileUploadService)
{
    private readonly ProductRepository _productRepository = productRepository;
    private readonly FileUploadService _fileUploadService = fileUploadService;

    public async Task<Response> GetAllAsync(int page, int pageSize)
    {
        var list = await _productRepository.GetAllAsync(page, pageSize);
        var response = new Response { StatusCode = 200, Message = "OK" };
        response.Data.Products = ProductMapper.MapListEntityToListDto(list);
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
        response.Data.Product = ProductMapper.MapEntityToDto(entity);
        return response;
    }

    public async Task<Response> CreateAsync(CreateProductRequest req)
    {
        // Upload image nếu có
        string? imagePath = null;
        if (req.Image != null)
        {
            var uploadResult = await _fileUploadService.UploadImageAsync(req.Image);
            if (!uploadResult.success)
            {
                return new Response 
                { 
                    StatusCode = 400, 
                    Message = uploadResult.error ?? "Failed to upload image" 
                };
            }
            imagePath = uploadResult.filePath;
        }

        var entity = new Product
        {
            ProductName = req.ProductName,
            Barcode = req.Barcode,
            Price = req.Price,
            Unit = req.Unit,
            ImagePath = imagePath,
            Status = true
        };

        if (Guid.TryParse(req.CategoryId, out var categoryId))
            entity.CategoryId = categoryId;
        if (Guid.TryParse(req.SupplierId, out var supplierId))
            entity.SupplierId = supplierId;

        await _productRepository.AddAsync(entity);

        var response = new Response { StatusCode = 201, Message = "Created" };
        response.Data.Product = ProductMapper.MapEntityToDto(entity);
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

        // Upload image mới nếu có
        if (req.Image != null)
        {
            var uploadResult = await _fileUploadService.UploadImageAsync(req.Image);
            if (!uploadResult.success)
            {
                return new Response 
                { 
                    StatusCode = 400, 
                    Message = uploadResult.error ?? "Failed to upload image" 
                };
            }

            // Xóa ảnh cũ
            _fileUploadService.DeleteImage(entity.ImagePath);
            
            // Cập nhật đường dẫn ảnh mới
            entity.ImagePath = uploadResult.filePath;
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
        response.Data.Product = ProductMapper.MapEntityToDto(entity);
        return response;
    }

    // Soft Delete
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

        // Xóa file ảnh khi soft delete (tùy chọn)
        // Nếu muốn giữ ảnh thì comment dòng này
        _fileUploadService.DeleteImage(entity.ImagePath);

        await _productRepository.SoftDeleteAsync(entity);
        response.StatusCode = 200;
        response.Message = "Deleted";
        return response;
    }
}