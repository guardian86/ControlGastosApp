namespace ControlGastos.Client;

using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.JSInterop;

public class AuthService
{
    private readonly IJSRuntime _js;
    private const string TokenKey = "jwt_token";

    public AuthService(IJSRuntime js, HttpClient http)
    {
        _js = js;
    }

    public async Task<string?> GetTokenAsync()
    {
        return await _js.InvokeAsync<string>("localStorage.getItem", TokenKey);
    }

    public async Task SetTokenAsync(string token)
    {
        await _js.InvokeVoidAsync("localStorage.setItem", TokenKey, token);
    }

    public async Task RemoveTokenAsync()
    {
        await _js.InvokeVoidAsync("localStorage.removeItem", TokenKey);
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var token = await GetTokenAsync();
        return !string.IsNullOrEmpty(token);
    }

    public static void AttachTokenToHttpClient(HttpClient httpClient, string? token)
    {
        if (!string.IsNullOrEmpty(token))
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        else
        {
            httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }

    public async Task LogoutAsync()
    {
        await RemoveTokenAsync();
    }

    public async Task<string?> GetUserNameAsync()
    {
        var token = await GetTokenAsync();
        if (string.IsNullOrEmpty(token)) return null;
        try
        {
            var parts = token.Split('.');
            if (parts.Length != 3) return null;
            var payload = parts[1];
            var json = System.Text.Encoding.UTF8.GetString(PadBase64(payload));
            using var doc = JsonDocument.Parse(json);
            if (doc.RootElement.TryGetProperty("unique_name", out var userNameProp))
                return userNameProp.GetString();
            if (doc.RootElement.TryGetProperty("name", out var nameProp))
                return nameProp.GetString();
            if (doc.RootElement.TryGetProperty("sub", out var subProp))
                return subProp.GetString();
            return null;
        }
        catch { return null; }
    }

    private static byte[] PadBase64(string base64)
    {
        int pad = 4 - (base64.Length % 4);
        if (pad < 4) base64 += new string('=', pad);
        return Convert.FromBase64String(base64.Replace('-', '+').Replace('_', '/'));
    }
}
