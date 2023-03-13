using Chat.Domain.Models.Authentication.ValueObjects;

namespace Chat.Domain.Models.Authentication.Aggregates;

public class TokenResponse
{
    public Token Token { get; set; } = null!;
    public RefreshToken RefreshToken { get; set; } = null!;
    public RefreshTokenExpiryTime RefreshTokenExpiryTime { get; set; } = null!;
}
