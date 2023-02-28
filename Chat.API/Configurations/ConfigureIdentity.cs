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
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
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
