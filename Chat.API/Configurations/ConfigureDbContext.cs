using Chat.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Chat.API.Configurations;

public static class ConfigureDbContext
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<ChatContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("ChatConnectionString"),
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(ChatContext).Assembly.FullName);
                    sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
                    sqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", ChatContext.DEFAULT_SCHEMA);
                });
        });
}
