using Backend.Dtos;
using Backend.Dtos.Requests;  
using Backend.Dtos.Responses;
using Backend.Models;
using Backend.Repositories;

namespace Backend.Services.Apis;

public class SupplierService(SupplierRepository supplierRepository)
{
    private readonly SupplierRepository _supplierRepository = supplierRepository;

    public async Task<Response> GetAllAsync(int page, int pageSize)
    {
        var list = await _supplierRepository.GetAllAsync(page, pageSize);
        var response = new Response { StatusCode = 200, Message = "OK" };
        response.Data.Suppliers = list.Select(s => new SupplierDto
        {
            Id = s.Id.ToString(),
            Name = s.Name,
            Phone = s.Phone,
            Email = s.Email,
            Address = s.Address
        }).ToList();
        return response;
    }

    public async Task<Response> GetByIdAsync(Guid id)
    {
        var entity = await _supplierRepository.GetByIdAsync(id);
        var response = new Response();
        if (entity == null)
        {
            response.StatusCode = 404;
            response.Message = "Supplier not found";
            return response;
        }

        response.StatusCode = 200;
        response.Message = "OK";
        response.Data.Supplier = new SupplierDto
        {
            Id = entity.Id.ToString(),
            Name = entity.Name,
            Phone = entity.Phone,
            Email = entity.Email,
            Address = entity.Address
        };
        return response;
    }

    public async Task<Response> CreateAsync(CreateSupplierRequest req)  // ← Đổi từ SupplierDto
    {
        var entity = new Supplier
        {
            Name = req.Name,
            Phone = req.Phone,
            Email = req.Email,
            Address = req.Address
        };

        await _supplierRepository.AddAsync(entity);

        var response = new Response { StatusCode = 201, Message = "Created" };
        response.Data.Supplier = new SupplierDto
        {
            Id = entity.Id.ToString(),
            Name = entity.Name,
            Phone = entity.Phone,
            Email = entity.Email,
            Address = entity.Address
        };
        return response;
    }

    public async Task<Response> UpdateAsync(Guid id, UpdateSupplierRequest req)  // ← Đổi từ SupplierDto
    {
        var entity = await _supplierRepository.GetByIdAsync(id);
        var response = new Response();
        if (entity == null)
        {
            response.StatusCode = 404;
            response.Message = "Supplier not found";
            return response;
        }

        entity.Name = req.Name;
        entity.Phone = req.Phone;
        entity.Email = req.Email;
        entity.Address = req.Address;

        await _supplierRepository.UpdateAsync(entity);

        response.StatusCode = 200;
        response.Message = "Updated";
        response.Data.Supplier = new SupplierDto
        {
            Id = entity.Id.ToString(),
            Name = entity.Name,
            Phone = entity.Phone,
            Email = entity.Email,
            Address = entity.Address
        };
        return response;
    }

    public async Task<Response> DeleteAsync(Guid id)
    {
        var entity = await _supplierRepository.GetByIdAsync(id);
        var response = new Response();
        if (entity == null)
        {
            response.StatusCode = 404;
            response.Message = "Supplier not found";
            return response;
        }

        await _supplierRepository.DeleteAsync(entity);
        response.StatusCode = 200;
        response.Message = "Deleted";
        return response;
    }
}