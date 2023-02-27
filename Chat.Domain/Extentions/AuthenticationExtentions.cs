using Chat.Domain.Models.Authentication.Aggregates;
using System.Security.Claims;

namespace Chat.Domain.Extentions;

public static class AuthenticationExtentions
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
