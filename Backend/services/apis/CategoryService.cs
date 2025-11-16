using Backend.Dtos;
using Backend.Dtos.Requests;
using Backend.Dtos.Responses;
using Backend.Models;
using Backend.Repositories;
using Backend.Utils.Mappers;

namespace Backend.Services.Apis;

public class CategoryService(CategoryRepository categoryRepository)
{
    private readonly CategoryRepository _categoryRepository = categoryRepository;

    public async Task<Response> GetAllAsync(int page, int pageSize, string? q)
    {
        var list = await _categoryRepository.GetAllAsync(page, pageSize, q);
        var totalCount = await _categoryRepository.GetTotalCountAsync(q);
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        // Get product counts for all categories
        var categoryIds = list.Select(c => c.Id).ToList();
        var productCounts = await _categoryRepository.GetProductCountsAsync(categoryIds);

        var response = new Response { StatusCode = 200, Message = "OK" };
        response.Data.Categories = CategoryMapper.MapListEntityToListDto(list, productCounts);
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
    // Allow updating categories regardless of current status (active/deleted)
    var entity = await _categoryRepository.GetByIdIgnoreStatusAsync(id);
        var response = new Response();
        if (entity == null)
        {
            response.StatusCode = 404;
            response.Message = "Category not found";
            return response;
        }

        // Get product count for this category
        var productCounts = await _categoryRepository.GetProductCountsAsync(new[] { id });
        var productCount = productCounts.GetValueOrDefault(id, 0);

        response.StatusCode = 200;
        response.Message = "OK";
        response.Data.Category = CategoryMapper.MapEntityToDto(entity, productCount);
        return response;
    }

    public async Task<Response> CreateAsync(CreateCategoryRequest req)
    {
        var response = new Response();
        if (await _categoryRepository.ExistsByNameAsync(req.CategoryName))
        {
            response.StatusCode = 409;
            response.Message = "Category name already exists";
            return response;
        }

        var entity = new Category 
        { 
            CategoryName = req.CategoryName,
            Description = req.Description,
            Status = req.IsActive ? CategoryStatus.Active : CategoryStatus.Deleted
        };
        await _categoryRepository.AddAsync(entity);

        response.StatusCode = 201;
        response.Message = "Created";
        response.Data.Category = CategoryMapper.MapEntityToDto(entity);
        return response;
    }

    public async Task<Response> UpdateAsync(Guid id, UpdateCategoryRequest req)
    {
        var response = new Response();
        // Allow updating categories regardless of current status (active/deleted)
        var entity = await _categoryRepository.GetByIdIgnoreStatusAsync(id);
        if (entity == null)
        {
            response.StatusCode = 404;
            response.Message = "Category not found";
            return response;
        }

        // Only validate and update name if provided and changed
        if (!string.IsNullOrWhiteSpace(req.CategoryName) &&
            !string.Equals(req.CategoryName, entity.CategoryName, StringComparison.OrdinalIgnoreCase))
        {
            if (await _categoryRepository.ExistsByNameAsync(req.CategoryName, excludeId: id))
            {
                response.StatusCode = 409;
                response.Message = "Category name already in use";
                return response;
            }
            entity.CategoryName = req.CategoryName;
        }

        // Update description only when provided (avoid unintentionally clearing)
        if (req.Description != null)
        {
            entity.Description = req.Description;
        }
        entity.Status = req.IsActive ? CategoryStatus.Active : CategoryStatus.Deleted;
        await _categoryRepository.UpdateAsync(entity);

        response.StatusCode = 200;
        response.Message = "Updated";
        response.Data.Category = CategoryMapper.MapEntityToDto(entity);
        return response;
    }

    // Soft Delete
    public async Task<Response> DeleteAsync(Guid id)
    {
        var response = new Response();
        // Use GetByIdIgnoreStatusAsync to allow deleting already-inactive categories
        var entity = await _categoryRepository.GetByIdIgnoreStatusAsync(id);
        if (entity == null)
        {
            response.StatusCode = 404;
            response.Message = "Category not found";
            return response;
        }

        if (await _categoryRepository.IsUsedByProductsAsync(id))
        {
            response.StatusCode = 409;
            response.Message = "Category is being used by products and cannot be deleted";
            return response;
        }

        await _categoryRepository.SoftDeleteAsync(entity);
        response.StatusCode = 200;
        response.Message = "Category deleted successfully";
        return response;
    }

    // Restore Category (nếu cần)
    public async Task<Response> RestoreAsync(Guid id)
    {
        var response = new Response();
        // Use GetByIdIgnoreStatusAsync to allow restoring deleted categories
        var entity = await _categoryRepository.GetByIdIgnoreStatusAsync(id);
        if (entity == null)
        {
            response.StatusCode = 404;
            response.Message = "Category not found";
            return response;
        }

        entity.Status = CategoryStatus.Active;
        await _categoryRepository.UpdateAsync(entity);

        response.StatusCode = 200;
        response.Message = "Category restored successfully";
        response.Data.Category = CategoryMapper.MapEntityToDto(entity);
        return response;
    }
}