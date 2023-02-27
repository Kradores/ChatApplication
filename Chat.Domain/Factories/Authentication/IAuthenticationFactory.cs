using Chat.Domain.Models.Authentication.Aggregates;
using Microsoft.AspNetCore.Identity;

namespace Chat.Domain.Factories.Authentication;

public interface IAuthenticationFactory
{
    Task<User> LogInAsync(User user, CancellationToken cancellationToken);
    Task<User> LogOutAsync(User user, CancellationToken cancellationToken);
    Task<IdentityResult> RegisterAsync(User user, CancellationToken cancellationToken);
    Task<bool> IsAuthenticatedAsync(User user, CancellationToken cancellationToken);
}
