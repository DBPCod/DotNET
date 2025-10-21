using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Dtos.Requests.User;
using Backend.Utils.Mappers;
using Backend.Services.Apis;
using Backend.Dtos.Responses;
using Backend.Utils.Customs; 
using Backend.Models;

namespace Backend.Controllers;

[Route("api/v1/users")]
[ApiController]
[Authorize]
public class UserController(UserService userService) : ControllerBase
{
    private readonly UserService _userService = userService;
    // GET /api/v1/users - Lấy danh sách users với pagination, search, filter
    [HttpGet]
    [Authorize(Roles = "ADMIN")] // Chỉ Admin mới được xem danh sách users
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersRequest request)
    {
        var response = new Response();

        try
        {
            var (users, totalCount) = await _userService.HandleGetUsersWithPagination(
                request.Page, 
                request.PageSize
            );

            // Apply search filter nếu có
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                users = users.Where(u => 
                    u.Username.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                    u.Email.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                    u.FullName.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            // Apply role filter nếu có
            if (!string.IsNullOrEmpty(request.Role) && Enum.TryParse<UserRole>(request.Role, out var role))
            {
                users = users.Where(u => u.Role == role).ToList();
            }

            // Apply sorting
            users = request.SortBy.ToLower() switch
            {
                "username" => request.SortOrder == "ASC" 
                    ? users.OrderBy(u => u.Username).ToList()
                    : users.OrderByDescending(u => u.Username).ToList(),
                "email" => request.SortOrder == "ASC"
                    ? users.OrderBy(u => u.Email).ToList()
                    : users.OrderByDescending(u => u.Email).ToList(),
                "fullname" => request.SortOrder == "ASC"
                    ? users.OrderBy(u => u.FullName).ToList()
                    : users.OrderByDescending(u => u.FullName).ToList(),
                _ => request.SortOrder == "ASC"
                    ? users.OrderBy(u => u.CreatedAt).ToList()
                    : users.OrderByDescending(u => u.CreatedAt).ToList()
            };

            var userDtos = UserMapper.MapListEntityToListDto(users);
            var totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);

            response.Message = "Users retrieved successfully";
            response.StatusCode = 200;
            response.Data.Users = userDtos;
            response.Data.Pagination = new PaginationInfo
            {
                CurrentPage = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                HasPreviousPage = request.Page > 1,
                HasNextPage = request.Page < totalPages
            };
        }
        catch (ExceptionCustom ex)
        {
            response.Message = ex.Message;
            response.StatusCode = ex.StatusCode;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.StatusCode = 500;
        }

        return StatusCode(response.StatusCode, response);
    }

    // GET /api/v1/users/{id} - Lấy user theo ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var response = new Response();

        try
        {
            var user = await _userService.HandleGetUserById(id);
            if (user == null)
            {
                response.Message = "User not found";
                response.StatusCode = 404;
                return StatusCode(response.StatusCode, response);
            }

            var userDto = UserMapper.MapEntityToDto(user);
            response.Message = "User retrieved successfully";
            response.StatusCode = 200;
            response.Data.User = userDto;
        }
        catch (ExceptionCustom ex)
        {
            response.Message = ex.Message;
            response.StatusCode = ex.StatusCode;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.StatusCode = 500;
        }

        return StatusCode(response.StatusCode, response);
    }

    // POST /api/v1/users - Tạo user mới
    [HttpPost]
    [Authorize(Roles = "ADMIN")] // Chỉ Admin mới được tạo user
    public async Task<IActionResult> CreateUser([FromForm] CreateUserRequest request)
    {
        var response = new Response();

        try
        {
            var user = await _userService.HandleCreateUser(
                request.Username,
                request.Email,
                request.Password,
                request.Role,
                request.FullName
            );

            if (user == null)
            {
                response.Message = "Failed to create user";
                response.StatusCode = 500;
                return StatusCode(response.StatusCode, response);
            }

            var userDto = UserMapper.MapEntityToDto(user);
            response.Message = "User created successfully";
            response.StatusCode = 201;
            response.Data.User = userDto;
        }
        catch (ExceptionCustom ex)
        {
            response.Message = ex.Message;
            response.StatusCode = ex.StatusCode;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.StatusCode = 500;
        }

        return StatusCode(response.StatusCode, response);
    }

    // PUT /api/v1/users/{id} - Cập nhật user
    [HttpPut("{id}")]
    [Authorize(Roles = "ADMIN")] // Chỉ Admin mới được cập nhật user
    public async Task<IActionResult> UpdateUser(Guid id, [FromForm] UpdateUserRequest request)
    {
        var response = new Response();

        try
        {
            var user = await _userService.HandleUpdateUser(
                id,
                request.Username,
                request.Email,
                request.FullName,
                request.Role
            );

            if (user == null)
            {
                response.Message = "User not found";
                response.StatusCode = 404;
                return StatusCode(response.StatusCode, response);
            }

            var userDto = UserMapper.MapEntityToDto(user);
            response.Message = "User updated successfully";
            response.StatusCode = 200;
            response.Data.User = userDto;
        }
        catch (ExceptionCustom ex)
        {
            response.Message = ex.Message;
            response.StatusCode = ex.StatusCode;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.StatusCode = 500;
        }

        return StatusCode(response.StatusCode, response);
    }

    // DELETE /api/v1/users/{id} - Xóa user
    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN")] // Chỉ Admin mới được xóa user
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var response = new Response();

        try
        {
            var success = await _userService.HandleDeleteUser(id);
            if (!success)
            {
                response.Message = "User not found";
                response.StatusCode = 404;
                return StatusCode(response.StatusCode, response);
            }

            response.Message = "User deleted successfully";
            response.StatusCode = 200;
        }
        catch (ExceptionCustom ex)
        {
            response.Message = ex.Message;
            response.StatusCode = ex.StatusCode;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.StatusCode = 500;
        }

        return StatusCode(response.StatusCode, response);
    }
}
