﻿using Chat.Domain.Models.Authentication.Aggregates;
using UserEntity = Chat.Infrastructure.Entities.User;
using Chat.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

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

    public Task<User> LogInAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<User> LogOutAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IdentityResult> RegisterAsync(User user, CancellationToken cancellationToken)
    {
        var entity = new UserEntity()
        {
            UserName = user.Username.Value
        };

        var result = await _userManager.CreateAsync(entity, user.Password.Value);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(entity, isPersistent: false);
        }

        return result;
    }
}
