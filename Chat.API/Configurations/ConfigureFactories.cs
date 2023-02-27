using Chat.Domain.Factories.Authentication;

namespace Chat.API.Configurations;

public static class ConfigureFactories
{
    public static IServiceCollection AddFactories(this IServiceCollection services) =>
        services
        .AddScoped<IAuthenticationFactory, AuthenticationFactory>();
}
