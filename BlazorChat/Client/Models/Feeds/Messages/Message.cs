namespace BlazorChat.Client.Models.Feeds.Messages;

public class Message
{
    public int Id { get; set; }
    public int ChatRoomId { get; set; }
    public string Text { get; set; } = null!;
    public string CreatedAt { get; set; } = null!;
    public string SenderName { get; set; } = null!;
    public string Status { get; set; } = null!;
}
