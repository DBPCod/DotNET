# Store Management System

Hệ thống quản lý cửa hàng được xây dựng bằng ASP.NET Core với Entity Framework Core và MySQL.

## Yêu cầu hệ thống

- .NET 9.0 SDK
- MySQL Server 8.0 hoặc cao hơn
- Visual Studio 2022 hoặc Visual Studio Code

## Cài đặt và chạy Backend

### 1. Cài đặt .NET 9.0 SDK

Tải và cài đặt .NET 9.0 SDK từ: https://dotnet.microsoft.com/download/dotnet/9.0

Kiểm tra cài đặt:
```bash
dotnet --version
```

### 2. Cài đặt MySQL

#### Trên Windows:
- Tải MySQL Community Server từ: https://dev.mysql.com/downloads/mysql/
- Cài đặt với mật khẩu root (để trống nếu muốn giống config hiện tại)

#### Trên macOS:
```bash
brew install mysql
brew services start mysql
```

#### Trên Ubuntu/Debian:
```bash
sudo apt update
sudo apt install mysql-server
sudo mysql_secure_installation
```

### 3. Cấu hình Database

1. Khởi động MySQL service
2. Tạo database (tùy chọn - ứng dụng sẽ tự tạo):
```sql
CREATE DATABASE store_management;
```

3. Kiểm tra connection string trong `Backend/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=127.0.0.1;Port=3306;Database=store_management;User=root;Password=;"
  }
}
```

**Lưu ý:** Nếu MySQL của bạn có mật khẩu, hãy thay đổi `Password=;` thành `Password=your_password;`

### 4. Chạy Backend

#### Cách 1: Sử dụng Command Line

1. Mở terminal/command prompt
2. Di chuyển đến thư mục Backend:
```bash
cd Backend
```

3. Restore packages:
```bash
dotnet restore
```

4. Chạy ứng dụng:
```bash
dotnet run
```

#### Cách 2: Sử dụng Visual Studio

1. Mở file `Backend.csproj` trong Visual Studio
2. Nhấn F5 hoặc click "Start Debugging"

#### Cách 3: Sử dụng Visual Studio Code

1. Mở thư mục Backend trong VS Code
2. Nhấn F5 hoặc sử dụng terminal tích hợp:
```bash
dotnet run
```

### 5. Kiểm tra ứng dụng

Sau khi chạy thành công, ứng dụng sẽ khả dụng tại:
- HTTP: http://localhost:5000
- HTTPS: https://localhost:5001

## Cấu trúc dự án

```
Backend/
├── Controllers/        # API Controllers
├── Models/            # Data Models
├── Contexts/          # Database Context
├── Migrations/        # EF Core Migrations
├── Data/              # Seed Data
├── Views/             # MVC Views
├── wwwroot/           # Static files
├── Program.cs         # Application entry point
└── appsettings.json   # Configuration
```

## Models có sẵn

- **Category**: Danh mục sản phẩm
- **Customer**: Khách hàng
- **Inventory**: Kho hàng
- **Order**: Đơn hàng
- **OrderItem**: Chi tiết đơn hàng
- **Payment**: Thanh toán
- **Product**: Sản phẩm
- **Promotion**: Khuyến mãi
- **Supplier**: Nhà cung cấp
- **User**: Người dùng

## Tính năng

- ✅ Auto Migration: Tự động tạo database và bảng khi khởi động
- ✅ Seed Data: Tự động thêm dữ liệu mẫu
- ✅ Entity Framework Core với MySQL
- ✅ MVC Pattern
- ✅ RESTful API ready

## Troubleshooting

### Lỗi kết nối MySQL
```
Unable to connect to any of the specified MySQL hosts
```
**Giải pháp:**
1. Kiểm tra MySQL service đã chạy chưa
2. Kiểm tra connection string trong appsettings.json
3. Kiểm tra firewall/port 3306

### Lỗi Migration
```
Unable to create an object of type 'AppDbContext'
```
**Giải pháp:**
1. Kiểm tra connection string
2. Chạy lại: `dotnet ef database update`

### Port đã được sử dụng
```
Unable to bind to https://localhost:5001
```
**Giải pháp:**
1. Thay đổi port trong `Properties/launchSettings.json`
2. Hoặc kill process đang sử dụng port:
```bash
# Windows
netstat -ano | findstr :5001
taskkill /PID <PID> /F

# macOS/Linux
lsof -ti:5001 | xargs kill
```

## Phát triển

### Thêm Migration mới
```bash
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

### Chạy ở chế độ Development
```bash
dotnet run --environment Development
```

### Build cho Production
```bash
dotnet build --configuration Release
dotnet publish --configuration Release
```

## Liên hệ

Nếu gặp vấn đề, vui lòng tạo issue hoặc liên hệ team phát triển.