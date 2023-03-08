using BlazorChat.Client.StateContainers;

namespace BlazorChat.Client.Configurations;

public static class ConfigureStateContainers
{
    public static IServiceCollection AddStateContainers(this IServiceCollection services)
    {
        services
            .AddSingleton<ChatRoomsStateContainer>()
            .AddScoped<ChatHubStateContainer>();
        return services;
    }
}
