using Chat.Domain.Models.ValueObjects;

namespace Chat.Domain.Models.Chat.Aggregates;

public class ChatRoom : IAggregateRoot
{
    private readonly List<Notification> _notifications;
    public ChatRoom() => _notifications = new();

    public ChatRoom(Id id, Name name, CreatedAt createdAt, List<Notification> notifications)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
        _notifications = notifications;
    }

    public Id Id { get; init; } = null!;
    public Name Name { get; init; } = null!;
    public CreatedAt CreatedAt { get; init; } = CreatedAt.Default;
    public ICollection<Notification> Notifications => _notifications;

    public void Add(Notification notification)
    {
        _notifications.Add(notification);
    }

    public void Remove(Notification notification)
    {
        _notifications.Remove(notification);
    }
}
