using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.JSInterop;
using Frontend.Models.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace Frontend.Services;

public class AuthService(HttpClient httpClient, IJSRuntime jsRuntime)
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IJSRuntime _jsRuntime = jsRuntime;
    private const string TokenKey = "auth_token";
    private const string UserKey = "auth_user";
    
    public bool IsLoading { get; private set; }
    public UserDto? CurrentUser { get; private set; }
    public string ErrorMessage { get; private set; } = "";

    // Login
    public async Task<AuthResponse<LoginData>?> LoginAsync(string usernameOrEmail, string password)
    {
        IsLoading = true;
        ErrorMessage = "";
        
        try
        {
            Console.WriteLine("Logging in user...");
            var formData = new MultipartFormDataContent
            {
                { new StringContent(usernameOrEmail), "UsernameOrEmail" },
                { new StringContent(password), "Password" }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/v1/auth/login")
            {
                Content = formData
            };
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);
            var result = await response.Content.ReadFromJsonAsync<AuthResponse<LoginData>>();

            if (result != null && result.StatusCode == 200 && result.Data?.User != null)
            {
                CurrentUser = result.Data.User;
                await SetUserAsync(result.Data.User);
            }
            
            Console.WriteLine("Login response received");
            return result;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Lỗi: {ex.Message}";
            Console.WriteLine(ex);
            return null;
        }
        finally
        {
            IsLoading = false;
        }
    }

    // Register
    public async Task<AuthResponse<object>?> RegisterAsync(string username, string email, string password, string confirmPassword)
    {
        IsLoading = true;
        ErrorMessage = "";
        
        try
        {
            Console.WriteLine("Registering user...");
            var formData = new MultipartFormDataContent
            {
                { new StringContent(username), "Username" },
                { new StringContent(email), "Email" },
                { new StringContent(password), "Password" },
                { new StringContent(confirmPassword), "ConfirmPassword" }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/v1/auth/register")
            {
                Content = formData
            };
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);
            var result = await response.Content.ReadFromJsonAsync<AuthResponse<object>>();

            if (result != null && result.StatusCode >= 400)
            {
                ErrorMessage = result.Message;
            }
            
            return result;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Lỗi: {ex.Message}";
            Console.WriteLine(ex);
            return null;
        }
        finally
        {
            IsLoading = false;
        }
    }

    // Send OTP
    public async Task<AuthResponse<object>?> SendOtpAsync(string email)
    {
        IsLoading = true;
        ErrorMessage = "";
        
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"/api/v1/auth/send-otp/{email}");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);
            var result = await response.Content.ReadFromJsonAsync<AuthResponse<object>>();
            
            if (result != null && result.StatusCode != 200)
            {
                ErrorMessage = result.Message;
            }
            
            return result;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Lỗi: {ex.Message}";
            return null;
        }
        finally
        {
            IsLoading = false;
        }
    }

    // Verify OTP
    public async Task<AuthResponse<object>?> VerifyOtpAsync(string email, string otp, bool isActivation)
    {
        IsLoading = true;
        ErrorMessage = "";
        
        try
        {
            var formData = new MultipartFormDataContent
            {
                { new StringContent(otp), "Otp" },
                { new StringContent(isActivation.ToString()), "IsActivation" }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"/api/v1/auth/verify-otp/{email}")
            {
                Content = formData
            };
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);
            var result = await response.Content.ReadFromJsonAsync<AuthResponse<object>>();
            
            if (result != null && result.StatusCode != 200)
            {
                ErrorMessage = result.Message;
            }
            
            return result;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Lỗi: {ex.Message}";
            return null;
        }
        finally
        {
            IsLoading = false;
        }
    }

    // Forgot Password (Reset Password)
    public async Task<AuthResponse<object>?> ForgotPasswordAsync(string email, string newPassword, string confirmPassword)
    {
        IsLoading = true;
        ErrorMessage = "";
        
        try
        {
            var formData = new MultipartFormDataContent
            {
                { new StringContent(newPassword), "Password" },
                { new StringContent(confirmPassword), "ConfirmPassword" }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"/api/v1/auth/forgot-password/{email}")
            {
                Content = formData
            };
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);
            var result = await response.Content.ReadFromJsonAsync<AuthResponse<object>>();
            
            if (result != null && result.StatusCode != 200)
            {
                ErrorMessage = result.Message;
            }
            
            return result;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Lỗi: {ex.Message}";
            return null;
        }
        finally
        {
            IsLoading = false;
        }
    }

    // Change Password
    public async Task<AuthResponse<object>?> ChangePasswordAsync(string email, string oldPassword, string newPassword, string confirmPassword)
    {
        IsLoading = true;
        ErrorMessage = "";
        
        try
        {
            var formData = new MultipartFormDataContent
            {
                { new StringContent(oldPassword), "OldPassword" },
                { new StringContent(newPassword), "NewPassword" },
                { new StringContent(confirmPassword), "ConfirmPassword" }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"/api/v1/auth/change-password/{email}")
            {
                Content = formData
            };
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);
            var result = await response.Content.ReadFromJsonAsync<AuthResponse<object>>();
            
            if (result != null && result.StatusCode != 200)
            {
                ErrorMessage = result.Message;
            }
            
            return result;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Lỗi: {ex.Message}";
            return null;
        }
        finally
        {
            IsLoading = false;
        }
    }

    // Logout
    public async Task<bool> LogoutAsync()
    {
        IsLoading = true;
        
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/v1/auth/logout");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);
            
            if (response.IsSuccessStatusCode)
            {
                CurrentUser = null;
                await RemoveUserAsync();
                await RemoveTokenAsync();
                return true;
            }
            
            return false;
        }
        catch
        {
            return false;
        }
        finally
        {
            IsLoading = false;
        }
    }

    // Refresh Token
    public async Task<bool> RefreshTokenAsync()
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/v1/auth/refresh-token");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    // LocalStorage Helpers
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

    public async Task<UserDto?> GetUserAsync()
    {
        try
        {
            var userJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", UserKey);
            if (string.IsNullOrEmpty(userJson))
                return null;
                
            return JsonSerializer.Deserialize<UserDto>(userJson);
        }
        catch
        {
            return null;
        }
    }

    public async Task SetUserAsync(UserDto user)
    {
        try
        {
            var userJson = JsonSerializer.Serialize(user);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", UserKey, userJson);
            CurrentUser = user;
        }
        catch
        {
            // Ignore errors
        }
    }

    public async Task RemoveUserAsync()
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", UserKey);
            CurrentUser = null;
        }
        catch
        {
            // Ignore errors
        }
    }

    // Initialize user from storage
    public async Task InitializeAsync()
    {
        CurrentUser = await GetUserAsync();
    }
}

