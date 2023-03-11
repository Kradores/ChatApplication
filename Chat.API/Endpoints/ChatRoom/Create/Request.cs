namespace Chat.API.Endpoints.ChatRoom.Create;

public class Request
{
    public string Name { get; set; } = null!;
    public List<string> UserIds { get; set; } = new List<string>();
}
