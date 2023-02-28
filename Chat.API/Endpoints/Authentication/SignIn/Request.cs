namespace Chat.API.Endpoints.Authentication.SignIn;

public class Request
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}
