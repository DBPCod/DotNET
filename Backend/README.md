# Backend README

## Giới thiệu

Backend này được xây dựng bằng ASP.NET Core, sử dụng .NET 9.0. Nó cung cấp các API cho hệ thống xác thực người dùng, quản lý người dùng, và tích hợp Redis cho caching và rate limiting. Dự án sử dụng Entity Framework Core cho database, JWT cho authentication, và hỗ trợ gửi email qua SMTP.

## Cấu trúc thư mục

```
backend/
├── appsettings.json                    # Cấu hình ứng dụng
├── Program.cs                          # Điểm vào của ứng dụng
├── backend.csproj                      # File dự án .NET
├── docker-compose.yml                  # Cấu hình Docker Compose
├── bin/                                # Thư mục build output
├── obj/                                # Thư mục object files
├── Properties/
│   └── launchSettings.json             # Cấu hình launch
├── configs/                            # Cấu hình ứng dụng
│   ├── ConfigureExtensions.cs          # Extension methods cho cấu hình
│   ├── GlobalUsing.cs                  # Global using directives
│   ├── JsonNullEmptyIgnoreConverter.cs # Converter JSON
│   ├── RedisHosted.cs                  # Service hosted cho Redis
│   └── Variable.cs                     # Biến môi trường và hằng số
├── contexts/                           # Database contexts
│   └── AppDbContext.cs                 # EF Core DbContext
├── controllers/                        # API Controllers
│   └── AuthController.cs               # Controller cho authentication
├── dtos/                               # Data Transfer Objects
│   ├── UserDto.cs                      # DTO cho User
│   ├── requests/                       # DTOs cho requests
│   │   ├── ChangePasswordRequest.cs
│   │   ├── ForgotPasswordRequest.cs
│   │   ├── LoginRequest.cs
│   │   ├── RegisterRequest.cs
│   │   └── VerifyOtpRequest.cs
│   └── responses/                      # DTOs cho responses
│       ├── Data.cs                     # Data wrapper cho responses
│       ├── Response.cs                 # Response wrapper
│       └── ServiceResult.cs            # Kết quả service
├── initializers/                       # Khởi tạo dữ liệu
│   └── RootUserInitializer.cs          # Khởi tạo user root
├── models/                             # Entity models
│   └── User.cs                         # Model User
├── repositories/                       # Data access layer
│   └── UserRepository.cs               # Repository cho User
├── services/                           # Business logic
│   ├── AuthService.cs                  # Service cho authentication
│   └── UserService.cs                  # Service cho user management
├── templates/                          # Templates
│   └── mails/                          # Email templates
│       └── otpTemplate.html            # Template OTP email
└── utils/                              # Utilities
    ├── customs/                        # Custom exceptions
    │   └── ExceptionCustom.cs
    └── helpers/                        # Helper classes
        ├── MailHelper.cs               # Helper gửi email
        ├── RedisHelper.cs              # Helper Redis
        └── mappers/                    # Object mappers
            └── UserMapper.cs           # Mapper cho User
```

## Cài đặt và chạy

### Yêu cầu hệ thống

- .NET 9.0 SDK
- MySQL Server
- Redis Server
- SMTP Server (cho gửi email)

### Hướng dẫn chạy MySQL với Docker

1. Đảm bảo Docker và Docker Compose đã được cài đặt.

2. Khởi chạy:

   ```bash
   docker-compose up -d 
   ```

3. Khởi chạy:

   ```bash
   docker ps
   ```

4. Tắt docker:

   ```bash
   docker-compose down -v
   ```

#### Chi tiết về Docker Compose

File `docker-compose.yml` định nghĩa các services:

- **mysql**: Database MySQL, với volume để persist data.

### Chạy mà không dùng Docker

1. Cài đặt .NET 9.0 SDK từ [microsoft.com](https://dotnet.microsoft.com/download/dotnet/9.0).

2. Cài đặt MySQL và Redis trên máy local.

3. Sao chép file `.env.example` thành `.env` và điền các biến môi trường.

4. Chạy lệnh:

   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```

Ứng dụng sẽ chạy trên port được cấu hình trong `PORT_HTTP` và `PORT_HTTPS`.

## Biến môi trường (Environment Variables)

Backend sử dụng các biến môi trường để cấu hình. Các biến này được định nghĩa trong `Variable.cs` và được truy cập qua `Variable.Enviroments`.

### Cách sử dụng Variable.EnvironmentVariables

Trong code, bạn truy cập các biến môi trường như sau:

```csharp
using Backend.Configs;

public class SomeService
{
    public void SomeMethod()
    {
        var jwtSecret = Variable.Enviroments.JWT_SECRET;
        var redisHost = Variable.Enviroments.REDIS_HOST;
        // ...
    }
}
```

## API Endpoints và Authorization

### Authorization

Backend sử dụng JWT (JSON Web Tokens) cho authentication. Để authorize một route:

1. Thêm attribute `[Authorize]` lên controller hoặc action method.

2. Trong request header, gửi `Authorization: Bearer <jwt-token>`.

Ví dụ:

```csharp
[ApiController]
[Route("api/[controller]")]
[Authorize] // Tất cả actions trong controller này yêu cầu authentication
public class ProtectedController : ControllerBase
{
    [HttpGet("protected")]
    public IActionResult GetProtectedData()
    {
        // Chỉ user đã đăng nhập mới truy cập được
        return Ok("Protected data");
    }

    [HttpGet("public")]
    [AllowAnonymous] // Override để cho phép truy cập không cần auth
    public IActionResult GetPublicData()
    {
        return Ok("Public data");
    }
}
```

### Các API Endpoints chính

- `POST /api/auth/register` - Đăng ký user mới
- `POST /api/auth/login` - Đăng nhập
- `POST /api/auth/forgot-password` - Quên mật khẩu
- `POST /api/auth/verify-otp` - Xác thực OTP
- `POST /api/auth/change-password` - Đổi mật khẩu
- `GET /api/auth/me` - Lấy thông tin user hiện tại (cần auth)

## Cách tổ chức một Controller

### Cấu trúc cơ bản

Mỗi controller nên:

1. Kế thừa từ `ControllerBase`
2. Sử dụng `[ApiController]` và `[Route]` attributes (thường là `api/v1/[controller]`)
3. Sử dụng dependency injection cho services trong constructor
4. Trả về `IActionResult` với `StatusCode(response.StatusCode, response)`
5. Sử dụng DTOs cho request và response
6. Sử dụng `[Authorize]` cho các route cần authentication

### Ví dụ Controller dựa trên AuthController

```csharp
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/v1/auth")]
[ApiController]
public class AuthController(AuthService authService, UserService userService) : ControllerBase
{
    private readonly AuthService _authService = authService;
    private readonly UserService _userService = userService;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegisterRequest request)
    {
        var response = await _authService.Register(request);
        return StatusCode(response.StatusCode, response);
    }
}
```

### Giải thích cấu trúc

- **Dependency Injection**: Services được inject qua constructor.
- **Request Handling**: Sử dụng `[FromForm]` cho form data, `[FromBody]` cho JSON.
- **Response**: Luôn trả về `StatusCode(response.StatusCode, response)` với `response` là object `Response`.
- **Authorization**: Sử dụng `[Authorize]` cho routes cần auth, có thể chỉ định roles.
- **Cookies**: Sử dụng `Response.Cookies` để set cookies cho tokens.
- **Error Handling**: Xử lý lỗi trong service layer, controller chỉ trả về response.

## Cách tổ chức một Service

### Cấu trúc cơ bản

Mỗi service nên:

1. Là một class với constructor injection cho dependencies
2. Chứa business logic, không trực tiếp truy cập database (sử dụng repositories)
3. Sử dụng try-catch để handle exceptions
4. Trả về `Response` object với `StatusCode`, `Message`, và `Data`
5. Sử dụng helpers như `RedisHelper`, `MailHelper` cho các tác vụ phụ
6. Methods thường là async và trả về `Task<Response>`

### Ví dụ Service dựa trên AuthService

```csharp
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Services;

public class AuthService(UserService userService, RedisHelper redisHelper)
{
    private readonly UserService _userService = userService;
    private readonly RedisHelper _redisHelper = redisHelper;

    public async Task<Response> Register(RegisterRequest request)
    {
        var response = new Response();

        try
        {
            if (string.IsNullOrEmpty(request.Email))
                throw new ExceptionCustom("Email is required");

            if (string.IsNullOrEmpty(request.Password))
                throw new ExceptionCustom("Password is required");

            if (string.IsNullOrEmpty(request.ConfirmPassword))
                throw new ExceptionCustom("Confirm Password is required");

            if (request.Password != request.ConfirmPassword)
                throw new ExceptionCustom("Passwords do not match");

            var user = await _userService.HandleCreateUser(request.Email, request.Password, "", "") ?? throw new ExceptionCustom(400, "Failed to create user");
            var userDto = UserMapper.MapEntityToDto(user);

            response.Message = "User registered successfully";
            response.StatusCode = 201;
            response.Data!.User = userDto;
        }
        catch (ExceptionCustom ex)
        {
            response.Message = ex.Message;
            response.StatusCode = ex.StatusCode;
            Console.WriteLine($">>> {ex.Message}");
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.StatusCode = 500;
            Console.WriteLine($">>> {ex.Message}");
        }

        return response;
    }
}
```

### Giải thích cấu trúc Service

- **Dependency Injection**: Inject các services và helpers qua constructor.
- **Business Logic**: Chứa logic xử lý, gọi repositories cho database operations.
- **Error Handling**: Sử dụng try-catch, throw `ExceptionCustom` cho lỗi known, catch generic Exception.
- **Response**: Trả về `Response` với `StatusCode`, `Message`, và `Data` (nếu có).
- **Helpers**: Sử dụng `RedisHelper` cho caching, `MailHelper` cho email, etc.
- **Private Methods**: Các helper methods như `HandleCreateAndStoreOtp` được đặt private.
- **Async/Await**: Tất cả methods I/O đều async.

### Response Structure

Tất cả responses đều sử dụng `Response<T>` wrapper:

```csharp
public class Response<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }
}
```

`Data` là instance của `Data.cs`, chứa các properties như `User`, `Users`, `Item`, `Items`, etc. Điều chỉnh theo nhu cầu của từng endpoint.

## DTOs: Request và Response

### Request DTOs

Mỗi API endpoint nên có DTO request riêng, kế thừa từ base request nếu cần.

Ví dụ trong `requests/`:

```csharp
public class CreateItemRequest
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    public int CategoryId { get; set; }

    public string? Description { get; set; }
}
```

### Response DTOs

Sử dụng `Data.cs` để wrap data trả về:

```csharp
public class Data
{
    public ItemDto? Item { get; set; }
    public ICollection<ItemDto>? Items { get; set; }
    // Thêm properties khác theo nhu cầu
}
```

## Testing

Để test API:

1. Sử dụng tools như Postman hoặc curl.

2. Đối với endpoints cần auth, lấy JWT token từ `/api/auth/login` trước.

Ví dụ curl:

```bash
# Login
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"user@example.com","password":"password"}'

# Sử dụng token cho protected endpoint
curl -X GET http://localhost:5000/api/auth/me \
  -H "Authorization: Bearer YOUR_JWT_TOKEN"
```

## Troubleshooting

### Lỗi kết nối Redis

- Đảm bảo Redis đang chạy và cấu hình đúng trong biến môi trường.
- Kiểm tra logs trong console để xem lỗi chi tiết.

### Lỗi database

- Đảm bảo MySQL đang chạy và connection string đúng.
- Chạy migrations nếu cần: `dotnet ef database update`

### Lỗi gửi email

- Kiểm tra cấu hình SMTP.
- Đối với Gmail, sử dụng App Password thay vì mật khẩu thường.

## Contributing

1. Fork repository
2. Tạo feature branch
3. Commit changes
4. Push và tạo Pull Request

## License

[Thêm license nếu có]
