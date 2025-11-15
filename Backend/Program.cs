using DotNetEnv;
using Backend.Data;
using Backend.Services;
using Swashbuckle.AspNetCore.Swagger;
Env.Load();

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ConfigureExtensions.ConfigureAllBuilder(builder);

// Đăng ký FileUploadService để xử lý upload ảnh

builder.Services.AddScoped<FileUploadService>();

WebApplication app = builder.Build();

// Tắt seed data
// await SeedData.SeedAsync(app);

// Configure the HTTP request pipeline - Swagger phải đặt TRƯỚC các middleware khác
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// QUAN TRỌNG: Cho phép serve static files (để hiển thị ảnh đã upload)
app.UseStaticFiles();

// Middleware
app.UseCors(Variable.Constants.MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

// Controllers
app.MapControllers();
app.MapGet("/", () => "Hello World!");

app.Run();