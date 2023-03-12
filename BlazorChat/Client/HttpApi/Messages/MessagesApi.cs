using BlazorChat.Client.Extensions;
using BlazorChat.Client.Models.Requests.ChatRooms;
using BlazorChat.Client.Models.Requests.Messages;
using BlazorChat.Client.Models.Responses.ChatRooms;
using BlazorChat.Client.Models.Responses.Messages;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;

namespace BlazorChat.Client.Requests.Messages;

public class MessagesApi
{
    private readonly HttpClient _httpClient;
    private readonly string _baseAddress;

    public MessagesApi(HttpClient httpClient, IWebAssemblyHostEnvironment environment)
    {
        _httpClient = httpClient;
        _baseAddress = environment.BaseAddress;
    }

    public async Task<MessagesResponse> GetAsync(MessagesRequest request)
    {
        var uri = new Uri(QueryHelpers.AddQueryString(_baseAddress + "message", request.ToDictionary()));
        var result = await _httpClient.GetAsync(uri);

        result.EnsureSuccessStatusCode();

        return await result.Content.ReadFromJsonAsync<MessagesResponse>()
            ?? throw new FormatException(nameof(MessagesResponse));
    }
}
