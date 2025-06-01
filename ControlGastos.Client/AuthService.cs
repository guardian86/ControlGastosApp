using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.JSInterop;

public class AuthService
{
    private readonly IJSRuntime _js;
    private readonly HttpClient _http;
    private const string TokenKey = "jwt_token";

    public AuthService(IJSRuntime js, HttpClient http)
    {
        _js = js;
        _http = http;
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

    public void AttachTokenToHttpClient(HttpClient httpClient, string? token)
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
}
