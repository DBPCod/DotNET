using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Frontend;
using Frontend.Services;
using Blazored.Toast;
using Blazored.Modal;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Cấu hình HttpClient với base URL của backend API và credentials
builder.Services.AddTransient<CookieHandler>();
builder.Services.AddScoped(sp => {
    var cookieHandler = sp.GetRequiredService<CookieHandler>();
    cookieHandler.InnerHandler = new HttpClientHandler();
    
    var client = new HttpClient(cookieHandler) 
    { 
        BaseAddress = new Uri("http://localhost:4040") // Thay bằng URL backend của mấy bro
    };
    return client;
});

// Đăng ký UserService
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddBlazoredToast();
builder.Services.AddScoped<PromotionService>();

// Đăng ký service cho toast và modal của blazored
builder.Services.AddBlazoredToast();
builder.Services.AddBlazoredModal();
await builder.Build().RunAsync();