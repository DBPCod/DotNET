using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Dtos.Requests.User;
using Backend.Dtos.Responses;
using Backend.Utils.Mappers;
using Backend.Utils.Customs;

namespace Backend.Controllers;

[Route("api/v1/users")]
[ApiController]
// [Authorize]
public class UserController(UserService userService) : ControllerBase
{
    private readonly UserService _userService = userService;

    // GET /api/v1/users - Lấy danh sách users với phân trang và filter
    [HttpGet]
    [Authorize(Roles = "ADMIN")]
    // [Authorize()]
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersRequest request)
    {
        var response = new Response();

        try
        {
            var (users, totalCount) = await _userService.HandleGetUsersWithPagination(
                request.Page,
                request.PageSize,
                request.Q,
                request.Role,
                request.Status
            );

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
                TotalPages = totalPages
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
    // [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var response = new Response();

        try
        {
            var user = await _userService.HandleGetUserById(id);
            if (user == null)
            {
                response.Message = "Không tìm thấy người dùng này";
                response.StatusCode = 404;
                return StatusCode(response.StatusCode, response);
            }

            var userDto = UserMapper.MapEntityToDto(user);
            response.Message = "Lấy người dùng thành công";
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
    // [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var response = new Response();

        try
        {
            var user = await _userService.HandleCreateUser(request);
            if (user == null)
            {
                response.Message = "Lỗi khi tạo người dùng";
                response.StatusCode = 500;
                return StatusCode(response.StatusCode, response);
            }

            var userDto = UserMapper.MapEntityToDto(user);
            response.Message = "Tạo người dùng thành công";
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
    // [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequest request)
    {
        var response = new Response();

        try
        {
            var user = await _userService.HandleUpdateUser(id, request);
            if (user == null)
            {
                response.Message = "Không tìm thấy người dùng này";
                response.StatusCode = 404;
                return StatusCode(response.StatusCode, response);
            }

            var userDto = UserMapper.MapEntityToDto(user);
            response.Message = "Cập nhật người dùng thành công";
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

    // DELETE /api/v1/users/{id} - Soft delete user
    [HttpDelete("{id}")]
    // [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var response = new Response();

        try
        {
            var success = await _userService.HandleSoftDeleteUser(id);
            if (!success)
            {
                response.Message = "Không tìm thấy người dùng này";
                response.StatusCode = 404;
                return StatusCode(response.StatusCode, response);
            }

            response.Message = "Khóa người dùng thành công";
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