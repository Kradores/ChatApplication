namespace Chat.Infrastructure.Entities;

public class ChatRoom
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}
