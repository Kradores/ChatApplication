namespace Chat.API.Endpoints.Chat.Create;

public class Request
{
    public string Name { get; set; } = null!;
    public List<string> UserIds { get; set; } = new List<string>();
}
