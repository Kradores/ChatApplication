using Chat.Domain.Models.Authentication.Aggregates;
using System.Security.Claims;

namespace Chat.Domain.Extensions;

public static class AuthenticationExtensions
{
    public static ClaimsPrincipal Convert(this User user)
    {
        var claims = new List<Claim>()
        {
            new Claim("username", user.Username.Value)
        };

        var identity = new ClaimsIdentity(claims);

        return new(identity);
    }
}
