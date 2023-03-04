using Chat.Infrastructure;
using Chat.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace Chat.API.Configurations;

public static class ConfigureIdentity
{
    public static IServiceCollection AddIdentityAuthentication(this IServiceCollection services)
    {
        services.AddIdentityCore<User>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 4;
        })
        .AddEntityFrameworkStores<ChatContext>()
        .AddUserManager<UserManager<User>>()
        .AddSignInManager<SignInManager<User>>()
        .AddDefaultTokenProviders();

        services.AddAuthentication(o =>
        {
            o.DefaultScheme = IdentityConstants.ApplicationScheme;
            o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        })
        .AddIdentityCookies(o => { });

        services.AddAuthorization();

        return services;
    }
}
