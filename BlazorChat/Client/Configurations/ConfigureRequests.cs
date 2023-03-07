using BlazorChat.Client.Requests.Chats;
using BlazorChat.Client.Requests.Users;

namespace BlazorChat.Client.Configurations;

public static class ConfigureRequests
{
    public static IServiceCollection AddApiRequests(this IServiceCollection services)
    {
        services.AddScoped<UserRequests>()
            .AddScoped<ChatRequests>();

        return services;
    }
}
