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
        var response = new Response { StatusCode = 200, Message = "OK" };
        response.Data.Categories = CategoryMapper.MapListEntityToListDto(list);
        // Optional: set pagination if needed later
        return response;
    }

    public async Task<Response> GetByIdAsync(Guid id)
    {
        var entity = await _categoryRepository.GetByIdAsync(id);
        var response = new Response();
        if (entity == null)
        {
            response.StatusCode = 404;
            response.Message = "Category not found";
            return response;
        }

        response.StatusCode = 200;
        response.Message = "OK";
        response.Data.Category = CategoryMapper.MapEntityToDto(entity);
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

        var entity = new Category { CategoryName = req.CategoryName };
        await _categoryRepository.AddAsync(entity);

        response.StatusCode = 201;
        response.Message = "Created";
        response.Data.Category = CategoryMapper.MapEntityToDto(entity);
        return response;
    }

    public async Task<Response> UpdateAsync(Guid id, UpdateCategoryRequest req)
    {
        var response = new Response();
        var entity = await _categoryRepository.GetByIdAsync(id);
        if (entity == null)
        {
            response.StatusCode = 404;
            response.Message = "Category not found";
            return response;
        }

        if (await _categoryRepository.ExistsByNameAsync(req.CategoryName, excludeId: id))
        {
            response.StatusCode = 409;
            response.Message = "Category name already in use";
            return response;
        }

        entity.CategoryName = req.CategoryName;
        await _categoryRepository.UpdateAsync(entity);

        response.StatusCode = 200;
        response.Message = "Updated";
        response.Data.Category = CategoryMapper.MapEntityToDto(entity);
        return response;
    }

    public async Task<Response> DeleteAsync(Guid id)
    {
        var response = new Response();
        var entity = await _categoryRepository.GetByIdAsync(id);
        if (entity == null)
        {
            response.StatusCode = 404;
            response.Message = "Category not found";
            return response;
        }

        if (await _categoryRepository.IsUsedByProductsAsync(id))
        {
            response.StatusCode = 409;
            response.Message = "Category is used by products";
            return response;
        }

        await _categoryRepository.DeleteAsync(entity);
        response.StatusCode = 200;
        response.Message = "Deleted";
        return response;
    }
}
