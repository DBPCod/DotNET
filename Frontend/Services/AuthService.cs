using Microsoft.JSInterop;

namespace Frontend.Services;

public class AuthService
{
    private readonly IJSRuntime _jsRuntime;
    private const string TokenKey = "auth_token";

    public AuthService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<string?> GetTokenAsync()
    {
        try
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TokenKey);
        }
        catch
        {
            return null;
        }
    }

    public async Task SetTokenAsync(string token)
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, token);
        }
        catch
        {
            // Ignore errors
        }
    }

    public async Task RemoveTokenAsync()
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
        }
        catch
        {
            // Ignore errors
        }
    }
}

