using Chat.Infrastructure.Repositories;
using Chat.Infrastructure.Repositories.Interfaces;

namespace Chat.API.Configurations;

public static class ConfigureRepositories
{
    public static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services
        .AddScoped<IUserRepository, UserRepository>()
        .AddScoped<IChatRepository, ChatRepository>();
}
