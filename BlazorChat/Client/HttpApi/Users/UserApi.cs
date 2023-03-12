using BlazorChat.Client.Models.Responses.Users;
using System.Net.Http.Json;

namespace BlazorChat.Client.Requests.Users;

public class UserApi
{
    private readonly HttpClient _httpClient;

    public UserApi(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UsersListResponse> GetAsync()
    {
        var users = await _httpClient.GetFromJsonAsync<UsersListResponse>("/user");

        if (users == null)
            return new();

        return users;
    }
}
