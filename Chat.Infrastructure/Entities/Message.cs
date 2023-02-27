namespace Chat.Infrastructure.Entities;

public class Message
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public int ChatRoomId { get; set; }
    public string Data { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public User User { get; set; } = null!;
    public ChatRoom ChatRoom { get; set; } = null!;
    public ICollection<MessageProperty> Properties { get; set; } = new List<MessageProperty>();
}
