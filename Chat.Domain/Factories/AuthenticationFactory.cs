using Chat.Domain.Factories.Interfaces;
using Chat.Domain.Models.Authentication.Aggregates;
using Chat.Domain.Models.Authentication.ValueObjects;
using Chat.Domain.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserEntity = Chat.Infrastructure.Entities.User;

namespace Chat.Domain.Factories;

public class AuthenticationFactory : IAuthenticationFactory
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly AppConfiguration _appConfig;

    public AuthenticationFactory(UserManager<UserEntity> userManager, IOptions<AppConfiguration> appConfig)
    {
        _userManager = userManager;
        _appConfig = appConfig.Value;
    }

    public Task<bool> IsAuthenticatedAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<TokenResponse?> SignInAsync(User model)
    {
        var entity = await _userManager.FindByNameAsync(model.Username.Value);
        if (entity == null) return null;
        var passwordValid = await _userManager.CheckPasswordAsync(entity, model.Password.Value);
        if (!passwordValid) return null;

        entity.RefreshToken = GenerateRefreshToken();
        entity.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
        await _userManager.UpdateAsync(entity);

        var token = await GenerateJwtAsync(entity);

        return new TokenResponse {
            Token = Token.From(token),
            RefreshToken = RefreshToken.From(entity.RefreshToken),
        };
    }

    public async Task SignOutAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<TokenResponse?> SignUpAsync(User user, CancellationToken cancellationToken)
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
            return await SignInAsync(user);
        }

        return null;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private string GenerateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
    {
        var token = new JwtSecurityToken(
           claims: claims,
           expires: DateTime.UtcNow.AddDays(2),
           signingCredentials: signingCredentials);
        var tokenHandler = new JwtSecurityTokenHandler();
        var encryptedToken = tokenHandler.WriteToken(token);
        return encryptedToken;
    }

    private async Task<string> GenerateJwtAsync(UserEntity user)
    {
        var token = GenerateEncryptedToken(GetSigningCredentials(), await GetClaimsAsync(user));
        return token;
    }

    private SigningCredentials GetSigningCredentials()
    {
        var secret = Encoding.UTF8.GetBytes(_appConfig.Secret);
        return new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);
    }

    private async Task<IEnumerable<Claim>> GetClaimsAsync(UserEntity user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);

        var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name, user.UserName)
            }
        .Union(userClaims);

        return claims;
    }
}
