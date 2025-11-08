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
            .FirstOrDefaultAsync(p => p.Id == id);
            
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
        try 
        {
            // Debug logging
            Console.WriteLine($"=== CREATE PRODUCT DEBUG ===");
            Console.WriteLine($"ProductName: {req.ProductName}");
            Console.WriteLine($"Price: {req.Price}");
            Console.WriteLine($"Image: {(req.Image != null ? req.Image.FileName : "NULL")}");
            Console.WriteLine($"Image Size: {(req.Image != null ? req.Image.Length : 0)}");
            
            // Validate required fields
            if (string.IsNullOrWhiteSpace(req.ProductName))
            {
                return new Response 
                { 
                    StatusCode = 400, 
                    Message = "Product name is required" 
                };
            }

            if (req.Price <= 0)
            {
                return new Response 
                { 
                    StatusCode = 400, 
                    Message = "Price must be greater than 0" 
                };
            }

            // Validate giới hạn của decimal(10,2) trong database
            if (req.Price > 99999999.99m)
            {
                return new Response 
                { 
                    StatusCode = 400, 
                    Message = "Price cannot exceed 99,999,999.99" 
                };
            }

            // Validate và parse CategoryId
            Guid? categoryId = null;
            if (!string.IsNullOrWhiteSpace(req.CategoryId))
            {
                if (!Guid.TryParse(req.CategoryId, out var catId))
                {
                    return new Response 
                    { 
                        StatusCode = 400, 
                        Message = "Invalid Category ID format" 
                    };
                }

                var categoryExists = await _context.Category
                    .AnyAsync(c => c.Id == catId && c.Status == CategoryStatus.Active);
                if (!categoryExists)
                {
                    return new Response 
                    { 
                        StatusCode = 400, 
                        Message = "Category not found or inactive" 
                    };
                }
                categoryId = catId;
            }

            // Validate và parse SupplierId
            Guid? supplierId = null;
            if (!string.IsNullOrWhiteSpace(req.SupplierId))
            {
                if (!Guid.TryParse(req.SupplierId, out var supId))
                {
                    return new Response 
                    { 
                        StatusCode = 400, 
                        Message = "Invalid Supplier ID format" 
                    };
                }

                var supplierExists = await _context.Supplier
                    .AnyAsync(s => s.Id == supId && s.Status == true);
                if (!supplierExists)
                {
                    return new Response 
                    { 
                        StatusCode = 400, 
                        Message = "Supplier not found or inactive" 
                    };
                }
                supplierId = supId;
            }

            // Upload ảnh nếu có
            string? imagePath = null;
            if (req.Image != null)
            {
                Console.WriteLine($"Uploading image: {req.Image.FileName}");
                var uploadResult = await _fileUploadService.UploadImageAsync(req.Image);
                Console.WriteLine($"Upload result - Success: {uploadResult.Success}, FilePath: {uploadResult.FilePath}, Error: {uploadResult.Error}");
                
                if (!uploadResult.Success)
                {
                    return new Response 
                    { 
                        StatusCode = 400, 
                        Message = uploadResult.Error ?? "Failed to upload image" 
                    };
                }
                imagePath = uploadResult.FilePath;
            }
            else
            {
                Console.WriteLine("No image provided");
            }

            Console.WriteLine($"Final ImagePath to save: {imagePath}");
            
            // Tạo entity mới
            var entity = new Product
            {
                ProductName = req.ProductName.Trim(),
                Barcode = req.Barcode?.Trim(),
                Price = req.Price,
                Unit = string.IsNullOrWhiteSpace(req.Unit) ? "pcs" : req.Unit.Trim(),
                ImagePath = imagePath,
                CategoryId = categoryId,
                SupplierId = supplierId,
                Status = req.Status,
                CreatedAt = DateTime.Now
            };
            
            Console.WriteLine($"Entity ImagePath before save: {entity.ImagePath}");

            // Lưu vào database
            await _productRepository.AddAsync(entity);

            // Reload với includes
            entity = await _context.Product
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.Id == entity.Id);

            if (entity == null)
            {
                // Rollback: xóa ảnh nếu lưu DB thất bại
                if (!string.IsNullOrEmpty(imagePath))
                {
                    _fileUploadService.DeleteImage(imagePath);
                }

                return new Response 
                { 
                    StatusCode = 500, 
                    Message = "Failed to create product in database" 
                };
            }

            var response = new Response { StatusCode = 201, Message = "Product created successfully" };
            response.Data.Product = ProductMapper.MapEntityToDto(entity);
            return response;
        }
        catch (DbUpdateException dbEx)
        {
            return new Response 
            { 
                StatusCode = 500,
                Message = "Database error occurred while creating product",
                Data = new ResponseData { Error = dbEx.InnerException?.Message ?? dbEx.Message }
            };
        }
        catch (Exception ex)
        {
            return new Response 
            { 
                StatusCode = 500,
                Message = "Failed to create product",
                Data = new ResponseData { Error = ex.Message }
            };
        }
    }

    public async Task<Response> UpdateAsync(Guid id, UpdateProductRequest req)
    {
        try
        {
            var entity = await _productRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return new Response 
                { 
                    StatusCode = 404, 
                    Message = "Product not found" 
                };
            }

            // Validate required fields
            if (string.IsNullOrWhiteSpace(req.ProductName))
            {
                return new Response 
                { 
                    StatusCode = 400, 
                    Message = "Product name is required" 
                };
            }

            if (req.Price <= 0)
            {
                return new Response 
                { 
                    StatusCode = 400, 
                    Message = "Price must be greater than 0" 
                };
            }

            // Validate giới hạn của decimal(10,2) trong database
            if (req.Price > 99999999.99m)
            {
                return new Response 
                { 
                    StatusCode = 400, 
                    Message = "Price cannot exceed 99,999,999.99" 
                };
            }

            // Validate và parse CategoryId
            Guid? categoryId = null;
            if (!string.IsNullOrWhiteSpace(req.CategoryId))
            {
                if (!Guid.TryParse(req.CategoryId, out var catId))
                {
                    return new Response 
                    { 
                        StatusCode = 400, 
                        Message = "Invalid Category ID format" 
                    };
                }

                var categoryExists = await _context.Category
                    .AnyAsync(c => c.Id == catId && c.Status == CategoryStatus.Active);
                if (!categoryExists)
                {
                    return new Response 
                    { 
                        StatusCode = 400, 
                        Message = "Category not found or inactive" 
                    };
                }
                categoryId = catId;
            }

            // Validate và parse SupplierId
            Guid? supplierId = null;
            if (!string.IsNullOrWhiteSpace(req.SupplierId))
            {
                if (!Guid.TryParse(req.SupplierId, out var supId))
                {
                    return new Response 
                    { 
                        StatusCode = 400, 
                        Message = "Invalid Supplier ID format" 
                    };
                }

                var supplierExists = await _context.Supplier
                    .AnyAsync(s => s.Id == supId && s.Status == true);
                if (!supplierExists)
                {
                    return new Response 
                    { 
                        StatusCode = 400, 
                        Message = "Supplier not found or inactive" 
                    };
                }
                supplierId = supId;
            }

            // Xử lý upload ảnh mới (nếu có)
            string? imagePath = entity.ImagePath;
            if (req.Image != null)
            {
                var uploadResult = await _fileUploadService.UploadImageAsync(req.Image);
                if (!uploadResult.Success)
                {
                    return new Response 
                    { 
                        StatusCode = 400, 
                        Message = uploadResult.Error ?? "Failed to upload image" 
                    };
                }
                
                // Xóa ảnh cũ nếu upload thành công
                if (!string.IsNullOrEmpty(entity.ImagePath))
                {
                    _fileUploadService.DeleteImage(entity.ImagePath);
                }
                
                imagePath = uploadResult.FilePath;
            }

            // Cập nhật thông tin
            entity.ProductName = req.ProductName.Trim();
            entity.Barcode = req.Barcode?.Trim();
            entity.Price = req.Price;
            entity.Unit = string.IsNullOrWhiteSpace(req.Unit) ? "pcs" : req.Unit.Trim();
            entity.CategoryId = categoryId;
            entity.SupplierId = supplierId;
            entity.Status = req.Status;
            entity.ImagePath = imagePath;

            await _productRepository.UpdateAsync(entity);

            // Reload với includes
            entity = await _context.Product
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (entity == null)
            {
                return new Response 
                { 
                    StatusCode = 500, 
                    Message = "Failed to update product" 
                };
            }

            var response = new Response { StatusCode = 200, Message = "Product updated successfully" };
            response.Data.Product = ProductMapper.MapEntityToDto(entity);
            return response;
        }
        catch (DbUpdateException dbEx)
        {
            return new Response 
            { 
                StatusCode = 500,
                Message = "Database error occurred while updating product",
                Data = new ResponseData { Error = dbEx.InnerException?.Message ?? dbEx.Message }
            };
        }
        catch (Exception ex)
        {
            return new Response 
            { 
                StatusCode = 500,
                Message = "Failed to update product",
                Data = new ResponseData { Error = ex.Message }
            };
        }
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
        response.Message = "Sản phẩm đã được ngừng bán";
        return response;
    }
}