using DotNetEnv;
using Backend.Data;
using Swashbuckle.AspNetCore.Swagger;
Env.Load();

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ConfigureExtensions.ConfigureAllBuilder(builder);

WebApplication app = builder.Build();

// Tắt seed data
// await SeedData.SeedAsync(app);

// Configure the HTTP request pipeline - Swagger phải đặt TRƯỚC các middleware khác
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware
app.UseCors(Variable.Constants.MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

// Controllers
app.MapControllers();
app.MapGet("/", () => "Hello World!");

app.Run();
