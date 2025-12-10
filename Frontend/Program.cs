using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Frontend;
using Frontend.Services;
using Blazored.Toast;
using Blazored.Modal;
using System.Net.Http;
using Microsoft.JSInterop;

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

// Đăng ký services dùng chung
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AuthService>();
// CartService cần AuthService, nên đăng ký sau AuthService
builder.Services.AddScoped<CartService>(sp => 
    new CartService(sp.GetRequiredService<IJSRuntime>(), sp.GetRequiredService<AuthService>()));
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<PromotionService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<OrderItemService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<SupplierService>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<InventoryService>();

// Đăng ký service cho toast và modal của blazored
builder.Services.AddBlazoredToast();
builder.Services.AddBlazoredModal();
await builder.Build().RunAsync();