namespace BlazorChat.Client.Models.Responses.ChatRooms;

public class ChatCreateResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int UnreadMessages { get; set; }
}
