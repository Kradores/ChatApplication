using BlazorChat.Client.Models.Requests.ChatRooms;
using BlazorChat.Client.Models.Responses.ChatRooms;
using System.Net.Http.Json;

namespace BlazorChat.Client.Requests.Chats;

public class ChatRequests
{
    private readonly HttpClient _httpClient;

    public ChatRequests(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ChatCreateResponse> CreateAsync(ChatCreateRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("/chat", request);

        result.EnsureSuccessStatusCode();

        return await result.Content.ReadFromJsonAsync<ChatCreateResponse>() 
            ?? throw new FormatException(nameof(ChatCreateResponse));
    }
}
