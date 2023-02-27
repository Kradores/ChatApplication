namespace Chat.Domain.Options;

public class JwtOptions
{
    public const string Jwt = "Jwt";

    public string Key { get; set; } = null!;
    public string Issuer { get; set; } = null!;
}
