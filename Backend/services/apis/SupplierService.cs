using Backend.Dtos;
using Backend.Dtos.Requests;  
using Backend.Dtos.Responses;
using Backend.Models;
using Backend.Repositories;

namespace Backend.Services.Apis;

public class SupplierService(SupplierRepository supplierRepository)
{
    private readonly SupplierRepository _supplierRepository = supplierRepository;

    // Lấy tất cả suppliers (cho admin)
    public async Task<Response> GetAllAsync(int page, int pageSize)
    {
        var (suppliers, totalCount) = await _supplierRepository.GetAllAsync(page, pageSize);
        var response = new Response { StatusCode = 200, Message = "OK" };
        
        response.Data.Suppliers = suppliers.Select(s => new SupplierDto
        {
            Id = s.Id.ToString(),
            Name = s.Name,
            Phone = s.Phone,
            Email = s.Email,
            Address = s.Address,
            Status = s.Status
        }).ToList();
        
        // Thêm thông tin pagination
        response.Data.Pagination = new PaginationInfo
        {
            CurrentPage = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
        };
        
        return response;
    }

    // Lấy chỉ suppliers đang hoạt động (cho dropdown)
    public async Task<Response> GetActiveAsync()
    {
        var list = await _supplierRepository.GetActiveAsync();
        var response = new Response { StatusCode = 200, Message = "OK" };
        response.Data.Suppliers = list.Select(s => new SupplierDto
        {
            Id = s.Id.ToString(),
            Name = s.Name,
            Phone = s.Phone,
            Email = s.Email,
            Address = s.Address,
            Status = s.Status
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
            Address = entity.Address,
            Status = entity.Status
        };
        return response;
    }

    public async Task<Response> CreateAsync(CreateSupplierRequest req)
    {
        var entity = new Supplier
        {
            Name = req.Name,
            Phone = req.Phone,
            Email = req.Email,
            Address = req.Address,
            Status = true // Mặc định là active
        };

        await _supplierRepository.AddAsync(entity);

        var response = new Response { StatusCode = 201, Message = "Created" };
        response.Data.Supplier = new SupplierDto
        {
            Id = entity.Id.ToString(),
            Name = entity.Name,
            Phone = entity.Phone,
            Email = entity.Email,
            Address = entity.Address,
            Status = entity.Status
        };
        return response;
    }

    public async Task<Response> UpdateAsync(Guid id, UpdateSupplierRequest req)
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
        entity.Status = req.Status; // Cập nhật trạng thái

        await _supplierRepository.UpdateAsync(entity);

        response.StatusCode = 200;
        response.Message = "Updated";
        response.Data.Supplier = new SupplierDto
        {
            Id = entity.Id.ToString(),
            Name = entity.Name,
            Phone = entity.Phone,
            Email = entity.Email,
            Address = entity.Address,
            Status = entity.Status
        };
        return response;
    }

    // Toggle Status (Bật/Tắt hoạt động)
    public async Task<Response> ToggleStatusAsync(Guid id)
    {
        var entity = await _supplierRepository.GetByIdAsync(id);
        var response = new Response();
        if (entity == null)
        {
            response.StatusCode = 404;
            response.Message = "Supplier not found";
            return response;
        }

        entity.Status = !entity.Status;
        await _supplierRepository.UpdateAsync(entity);

        response.StatusCode = 200;
        response.Message = entity.Status ? "Supplier activated" : "Supplier deactivated";
        response.Data.Supplier = new SupplierDto
        {
            Id = entity.Id.ToString(),
            Name = entity.Name,
            Phone = entity.Phone,
            Email = entity.Email,
            Address = entity.Address,
            Status = entity.Status
        };
        return response;
    }

    // Soft Delete
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

        await _supplierRepository.SoftDeleteAsync(entity);
        response.StatusCode = 200;
        response.Message = "Supplier deactivated";
        return response;
    }
}