using Backend.Dtos;
using Backend.Dtos.Requests;
using Backend.Dtos.Responses;
using Backend.Models;
using Backend.Repositories;
using Backend.Services;
using Backend.Utils.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Apis;

public class ProductService(ProductRepository productRepository, FileUploadService fileUploadService, AppDbContext context)
{
    private readonly ProductRepository _productRepository = productRepository;
    private readonly FileUploadService _fileUploadService = fileUploadService;
    private readonly AppDbContext _context = context;

    public async Task<Response> GetAllAsync(
        int page, 
        int pageSize,
        string? searchQuery = null,
        string? categoryId = null,
        string? supplierId = null,
        bool? status = null)
    {
        Guid? categoryGuid = null;
        if (!string.IsNullOrEmpty(categoryId) && Guid.TryParse(categoryId, out var catId))
        {
            categoryGuid = catId;
        }

        Guid? supplierGuid = null;
        if (!string.IsNullOrEmpty(supplierId) && Guid.TryParse(supplierId, out var supId))
        {
            supplierGuid = supId;
        }

        var (products, totalCount) = await _productRepository.GetAllWithFilterAsync(
            page, 
            pageSize, 
            searchQuery, 
            categoryGuid, 
            supplierGuid, 
            status
        );
        
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        
        var response = new Response { StatusCode = 200, Message = "OK" };
        response.Data.Products = ProductMapper.MapListEntityToListDto(products);
        response.Data.Pagination = new PaginationInfo
        {
            CurrentPage = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };
        
        return response;
    }

    public async Task<Response> GetByIdAsync(Guid id)
    {
        var entity = await _context.Product
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .FirstOrDefaultAsync(p => p.Id == id && p.Status == true);
            
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

        // Reload with includes
        entity = await _context.Product
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .FirstOrDefaultAsync(p => p.Id == entity.Id);

        var response = new Response { StatusCode = 201, Message = "Created" };
        response.Data.Product = ProductMapper.MapEntityToDto(entity!);
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

        // Reload with includes
        entity = await _context.Product
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .FirstOrDefaultAsync(p => p.Id == id);

        response.StatusCode = 200;
        response.Message = "Updated";
        response.Data.Product = ProductMapper.MapEntityToDto(entity!);
        return response;
    }

    // Soft Delete - Đổi status thành false (ngừng bán)
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

        // Chỉ đổi status thành false, KHÔNG xóa ảnh
        await _productRepository.SoftDeleteAsync(entity);
        
        response.StatusCode = 200;
        response.Message = "Product status updated to inactive";
        return response;
    }
}