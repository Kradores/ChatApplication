using Chat.Domain.Factories;
using Chat.Domain.Factories.Interfaces;

namespace Chat.API.Configurations;

public static class ConfigureFactories
{
    public static IServiceCollection AddFactories(this IServiceCollection services) =>
        services
        .AddScoped<IAuthenticationFactory, AuthenticationFactory>()
        .AddScoped<IChatFactory, ChatFactory>()
        .AddScoped<IUserFactory, UserFactory>();
}
