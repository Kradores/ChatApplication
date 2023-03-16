using Chat.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Chat.API.Configurations;

public static class ConfigureDbContext
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ChatContext>(options =>
        {
            options.EnableSensitiveDataLogging();
            options.UseSqlServer(configuration.GetConnectionString("ChatConnectionString"),
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(ChatContext).Assembly.FullName);
                    sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
                    sqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", ChatContext.DEFAULT_SCHEMA);
                });
        }, ServiceLifetime.Transient);

        services.AddDbContext<AuthenticationContext>(options =>
        {
            options.EnableSensitiveDataLogging();
            options.UseSqlServer(configuration.GetConnectionString("ChatConnectionString"),
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(AuthenticationContext).Assembly.FullName);
                    sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
                    sqlOptions.MigrationsHistoryTable("__EFMigrationsHistory");
                });
        });

        return services;
    }

    public static IApplicationBuilder MigrateAllDbContexts(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        MigrateDbContext<AuthenticationContext>(serviceScope);
        MigrateDbContext<ChatContext>(serviceScope);

        return app;
    }

    private static void MigrateDbContext<T>(IServiceScope serviceScope) where T : DbContext
    {
        var context = serviceScope.ServiceProvider.GetRequiredService<T>();
        context.Database.Migrate();
    }
        
}
