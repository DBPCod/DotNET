using Backend.Contexts;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Đăng ký DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

// Add services to the container
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Tự động migrate (tạo DB và bảng nếu chưa có)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Configure middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// // Gọi hàm SeedData
// await Backend.Data.SeedData.SeedAsync(app);
app.Run();
