namespace Chat.API.Endpoints.Authentication.SignIn;

public class Response
{
    public string Token { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public DateTime RefreshTokenExpiryTime { get; set; }
}
