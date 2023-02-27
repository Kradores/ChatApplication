namespace Chat.Infrastructure.Entities;

public class Notification
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public int ChatRoomId { get; set; }
    public int UnreadMessages { get; set; } = 0;
    public ChatRoom ChatRoom { get; set; } = null!;
}
