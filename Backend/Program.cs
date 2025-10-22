using DotNetEnv;

Env.Load();

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ConfigureExtensions.ConfigureAllBuilder(builder);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserRepository>();

WebApplication app = builder.Build();

// Middleware
app.UseCors(Variable.Constants.MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend API v1");
});

// Controllers
app.MapControllers();
app.MapGet("/", () => "Hello World!");

app.Run();
