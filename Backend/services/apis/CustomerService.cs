using Backend.Dtos.Requests;
using Backend.Dtos.Responses;
using Backend.Dtos;
using Backend.Models;
using Backend.Repositories;

namespace Backend.Services.Apis;

public class CustomerService
{
    private readonly CustomerRepository _repo;

    public CustomerService(CustomerRepository repo)
    {
        _repo = repo;
    }

    public async Task<Response> GetAllAsync(int page, int pageSize, string? search = null, string? status = null)
    {
        var list = await _repo.GetAllAsync(page, pageSize, search, status);
        var response = new Response { StatusCode = 200, Message = "OK" };
        response.Data.Customers = list.Select(c => new CustomerDto {
            Id = c.Id.ToString(),
            CustomerId = c.CustomerId,
            Name = c.Name,
            Phone = c.Phone,
            Email = c.Email,
            Address = c.Address,
            Status = c.Status,
            CreatedAt = c.CreatedAt
        }).ToList();
        return response;
    }

    public async Task<Response> GetByIdAsync(Guid id)
    {
        var entity = await _repo.GetByIdAsync(id);
        var response = new Response();
        if (entity == null)
        {
            response.StatusCode = 404;
            response.Message = "Customer not found";
            return response;
        }

        response.StatusCode = 200;
        response.Message = "OK";
        response.Data.Customer = new CustomerDto {
            Id = entity.Id.ToString(),
            CustomerId = entity.CustomerId,
            Name = entity.Name,
            Phone = entity.Phone,
            Email = entity.Email,
            Address = entity.Address,
            Status = entity.Status,
            CreatedAt = entity.CreatedAt
        };
        return response;
    }

    public async Task<Response> CreateAsync(CreateCustomerRequest req)
    {
        // Tạo CustomerId tự động
        var lastCustomer = await _repo.GetLastCustomerAsync();
        var nextNumber = 1;
        
        if (lastCustomer?.CustomerId != null && lastCustomer.CustomerId.StartsWith("CUS"))
        {
            var numberPart = lastCustomer.CustomerId.Substring(3);
            if (int.TryParse(numberPart, out int lastNumber))
            {
                nextNumber = lastNumber + 1;
            }
        }
        
        var customerId = $"CUS{nextNumber:D3}";
        
        var customer = new Customer {
            CustomerId = customerId,
            Name = req.Name,
            Phone = req.Phone,
            Email = req.Email,
            Address = req.Address,
            Status = req.Status ?? "ACTIVE"
        };
        await _repo.AddAsync(customer);
        var response = new Response { StatusCode = 201, Message = "Created" };
        response.Data.Customer = new CustomerDto {
            Id = customer.Id.ToString(),
            CustomerId = customer.CustomerId,
            Name = customer.Name,
            Phone = customer.Phone,
            Email = customer.Email,
            Address = customer.Address,
            Status = customer.Status,
            CreatedAt = customer.CreatedAt
        };
        return response;
    }

    public async Task<Response> UpdateAsync(Guid id, UpdateCustomerRequest req)
    {
        var entity = await _repo.GetByIdAsync(id);
        var response = new Response();
        if (entity == null)
        {
            response.StatusCode = 404;
            response.Message = "Customer not found";
            return response;
        }

        entity.Name = req.Name;
        entity.Phone = req.Phone;
        entity.Email = req.Email;
        entity.Address = req.Address;
        if (req.Status != null)
        {
            entity.Status = req.Status;
        }

        await _repo.UpdateAsync(entity);

        response.StatusCode = 200;
        response.Message = "Updated";
        response.Data.Customer = new CustomerDto {
            Id = entity.Id.ToString(),
            CustomerId = entity.CustomerId,
            Name = entity.Name,
            Phone = entity.Phone,
            Email = entity.Email,
            Address = entity.Address,
            Status = entity.Status,
            CreatedAt = entity.CreatedAt
        };
        return response;
    }

    public async Task<Response> DeleteAsync(Guid id)
    {
        var entity = await _repo.GetByIdAsync(id);
        var response = new Response();
        if (entity == null)
        {
            response.StatusCode = 404;
            response.Message = "Customer not found";
            return response;
        }

        await _repo.DeleteAsync(entity);
        response.StatusCode = 200;
        response.Message = "Deleted";
        return response;
    }
}
