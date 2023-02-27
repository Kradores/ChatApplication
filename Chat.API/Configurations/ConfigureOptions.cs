using Chat.Domain.Options;

namespace Chat.API.Configurations;

public static class ConfigureOptions
{
    public static WebApplicationBuilder AddOptions(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<JwtOptions>(
            builder.Configuration.GetSection(JwtOptions.Jwt));

        return builder;
    }
}
