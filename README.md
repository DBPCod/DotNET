1. Mục tiêu dự án

Xây dựng ứng dụng quản lý (ví dụ: store management / squad planner) có:

Giao diện client tương tác ngay trên trình duyệt bằng Blazor WASM

Backend cung cấp API/MVC bằng ASP.NET Core (MVC + Web API)

Lưu trữ dữ liệu bằng MySQL (chạy trên Laragon)

2. Kiến trúc & công nghệ

Frontend: Blazor WebAssembly (Client project)

Backend: ASP.NET Core MVC / Web API (Server project)

ORM: Entity Framework Core (EF Core)

DB: MySQL (Connector: Pomelo.EntityFrameworkCore.MySql hoặc MySql.Data.EntityFrameworkCore)

Local DB manager: Laragon (kèm HeidiSQL / phpMyAdmin)

Công cụ: dotnet CLI, Visual Studio hoặc VS Code
