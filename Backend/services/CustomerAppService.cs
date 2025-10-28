using Backend.Dtos.Requests;
using Backend.Dtos.Responses;
using Backend.Dtos;
using Backend.Models;
using Backend.Repositories;

namespace Backend.Services;

public class CustomerAppService
{
    private readonly CustomerRepository _repo;

    public CustomerAppService(CustomerRepository repo)
    {
        _repo = repo;
    }

    public async Task<Response> GetAllAsync(int page, int pageSize)
    {
        var list = await _repo.GetAllAsync(page, pageSize);
        var response = new Response { StatusCode = 200, Message = "OK" };
        response.Data.Customers = list.Select(c => new CustomerDto {
            Id = c.Id.ToString(),
            Name = c.Name,
            Phone = c.Phone,
            Email = c.Email,
            Address = c.Address,
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
            Name = entity.Name,
            Phone = entity.Phone,
            Email = entity.Email,
            Address = entity.Address,
            CreatedAt = entity.CreatedAt
        };
        return response;
    }

    public async Task<Response> CreateAsync(CreateCustomerRequest req)
    {
        var customer = new Customer {
            Name = req.Name,
            Phone = req.Phone,
            Email = req.Email,
            Address = req.Address
        };
        await _repo.AddAsync(customer);
        var response = new Response { StatusCode = 201, Message = "Created" };
        response.Data.Customer = new CustomerDto {
            Id = customer.Id.ToString(),
            Name = customer.Name,
            Phone = customer.Phone,
            Email = customer.Email,
            Address = customer.Address,
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

        await _repo.UpdateAsync(entity);

        response.StatusCode = 200;
        response.Message = "Updated";
        response.Data.Customer = new CustomerDto {
            Id = entity.Id.ToString(),
            Name = entity.Name,
            Phone = entity.Phone,
            Email = entity.Email,
            Address = entity.Address,
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
