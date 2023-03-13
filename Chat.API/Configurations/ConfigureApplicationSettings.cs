using Chat.Domain.Options;

namespace Chat.API.Configurations;

public static class ConfigureApplicationSettings
{
    internal static AppConfiguration GetApplicationSettings(
           this IServiceCollection services,
           IConfiguration configuration)
    {
        var applicationSettingsConfiguration = configuration.GetSection(nameof(AppConfiguration));
        services.Configure<AppConfiguration>(applicationSettingsConfiguration);
        return applicationSettingsConfiguration.Get<AppConfiguration>();
    }
}
