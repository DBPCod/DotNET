using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Frontend;
using Frontend.Services;
using Blazored.Toast;
using Blazored.Modal;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Cấu hình HttpClient với base URL của backend API
builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri("http://localhost:4040") // Thay bằng URL backend của mấy bro
});

// Đăng ký UserService
builder.Services.AddScoped<UserService>();

// Đăng ký service cho toast và modal của blazored
builder.Services.AddBlazoredToast();
builder.Services.AddBlazoredModal();
await builder.Build().RunAsync();
