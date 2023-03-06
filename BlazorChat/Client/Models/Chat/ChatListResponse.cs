namespace BlazorChat.Client.Models.Chat;

public class ChatListResponse
{
    public List<ChatRoom> Rooms { get; set; } = new List<ChatRoom>();
}
