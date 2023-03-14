using BlazorChat.Client.Models.Requests.Authentication;
using BlazorChat.Client.Models.Responses.Authentication;
using BlazorChat.Client.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;

namespace BlazorChat.Client.HttpApi.Authentication;

public class AuthenticationApi
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AuthenticationApi(
        HttpClient httpClient,
        ILocalStorageService localStorage,
        AuthenticationStateProvider authenticationStateProvider)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<ClaimsPrincipal> CurrentUser()
    {
        var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
        return state.User;
    }

    public async Task<bool> LoginAsync(SignInRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("/sign-in", request);

        if (result.IsSuccessStatusCode)
        {
            await ApplyResponse(result);
        }

        return result.IsSuccessStatusCode;
    }

    public async Task LogoutAsync()
    {
        await _localStorage.RemoveItemAsync("authToken");
        await _localStorage.RemoveItemAsync("refreshToken");
        ((CustomAuthenticationStateProvider)_authenticationStateProvider).SignOut();
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    public async Task<bool> RegisterUserAsync(SignUpRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("/sign-up", request);

        if (result.IsSuccessStatusCode)
        {
            await ApplyResponse(result);
        }

        return result.IsSuccessStatusCode;
    }

    private async Task ApplyResponse(HttpResponseMessage result)
    {
        var response = await result.Content.ReadFromJsonAsync<TokenResponse>()
            ?? throw new FormatException(nameof(TokenResponse));

        await _localStorage.SetItemAsync("authToken", response.Token);
        await _localStorage.SetItemAsync("refreshToken", response.RefreshToken);

        await ((CustomAuthenticationStateProvider)_authenticationStateProvider).StateChangedAsync();

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Token);
    }

    public async Task<string> RefreshToken()
    {
        //var token = await _localStorage.GetItemAsync<string>("authToken");
        //var refreshToken = await _localStorage.GetItemAsync<string>("refreshToken");

        //var response = await _httpClient.PostAsJsonAsync(Routes.TokenEndpoints.Refresh, new RefreshTokenRequest { Token = token, RefreshToken = refreshToken });

        //var result = await response.ToResult<TokenResponse>();

        //token = result.Data.Token;
        //refreshToken = result.Data.RefreshToken;
        //await _localStorage.SetItemAsync("authToken", token);
        //await _localStorage.SetItemAsync("refreshToken", refreshToken);
        //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //return token;

        return default;
    }
}
