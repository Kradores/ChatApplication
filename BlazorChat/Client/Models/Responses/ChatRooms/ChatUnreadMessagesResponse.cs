namespace BlazorChat.Client.Models.Responses.ChatRooms;

public class ChatUnreadMessagesResponse
{
    public int ChatId { get; set; }
    public string UserId { get; set; } = null!;
    public int UnreadMessages { get; set; }
}
