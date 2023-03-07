namespace BlazorChat.Client.Models.Requests.ChatRooms;

public class ChatCreateRequest
{
    public string Name { get; set; } = null!;
    public List<string> UserIds { get; set; } = new List<string>();
}
