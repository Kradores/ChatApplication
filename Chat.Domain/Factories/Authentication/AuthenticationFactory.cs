using Chat.Domain.Models.Authentication.Aggregates;
using Microsoft.AspNetCore.Identity;
using UserEntity = Chat.Infrastructure.Entities.User;

namespace Chat.Domain.Factories.Authentication;

public class AuthenticationFactory : IAuthenticationFactory
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signInManager;

    public AuthenticationFactory(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public Task<bool> IsAuthenticatedAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<SignInResult> SignInAsync(User user)
    {
        return await _signInManager.PasswordSignInAsync(user.Username.Value, user.Password.Value, isPersistent: false, lockoutOnFailure: false);
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<IdentityResult> SignUpAsync(User user, CancellationToken cancellationToken)
    {
        var entity = new UserEntity()
        {
            UserName = user.Username.Value,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };

        var result = await _userManager.CreateAsync(entity, user.Password.Value);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(entity, isPersistent: false);
        }

        return result;
    }
}
