using Chat.Domain.Models.Authentication.Aggregates;
using Microsoft.AspNetCore.Identity;

namespace Chat.Domain.Factories.Interfaces;

public interface IAuthenticationFactory
{
    Task<SignInResult> SignInAsync(User user);
    Task SignOutAsync();
    Task<IdentityResult> SignUpAsync(User user, CancellationToken cancellationToken);
    Task<bool> IsAuthenticatedAsync(User user, CancellationToken cancellationToken);
}
