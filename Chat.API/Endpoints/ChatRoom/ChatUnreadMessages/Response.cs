namespace Chat.API.Endpoints.ChatRoom.ChatUnreadMessages;

public class Response
{
    public int ChatId { get; set; }
    public string UserId { get; set; } = null!;
    public int UnreadMessages { get; set; }
}
