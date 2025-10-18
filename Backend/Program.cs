using DotNetEnv;

Env.Load();

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ConfigureExtensions.ConfigureAllBuilder(builder);

WebApplication app = builder.Build();

// Middleware
app.UseCors(Variable.Constants.MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

// Controllers
app.MapControllers();
app.MapGet("/", () => "Hello World!");

// // Gọi hàm SeedData
// await Backend.Data.SeedData.SeedAsync(app);

app.Run();
