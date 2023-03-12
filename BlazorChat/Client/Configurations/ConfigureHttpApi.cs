using BlazorChat.Client.Requests.Chats;
using BlazorChat.Client.Requests.Messages;
using BlazorChat.Client.Requests.Users;

namespace BlazorChat.Client.Configurations;

public static class ConfigureHttpApi
{
    public static IServiceCollection AddApiRequests(this IServiceCollection services)
    {
        services.AddScoped<UserApi>()
            .AddScoped<ChatApi>()
            .AddScoped<MessagesApi>();

        return services;
    }
}
