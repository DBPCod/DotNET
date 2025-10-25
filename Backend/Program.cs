using DotNetEnv;
using Backend.Data;
Env.Load();

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ConfigureExtensions.ConfigureAllBuilder(builder);

WebApplication app = builder.Build();

await SeedData.SeedAsync(app);

// Middleware
app.UseCors(Variable.Constants.MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

// Controllers
app.MapControllers();
app.MapGet("/", () => "Hello World!");

app.Run();
