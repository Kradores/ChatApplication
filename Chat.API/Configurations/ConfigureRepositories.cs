using Chat.Domain.Factories.Authentication;
using Chat.Infrastructure.Repositories;

namespace Chat.API.Configurations;

public static class ConfigureRepositories
{
    public static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services
        .AddScoped<IUserRepository, UserRepository>();
}
