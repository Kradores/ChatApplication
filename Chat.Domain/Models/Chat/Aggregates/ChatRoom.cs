using Chat.Domain.Models.Authentication.Aggregates;
using Chat.Domain.Models.ValueObjects;

namespace Chat.Domain.Models.Chat.Aggregates;

public class ChatRoom : IAggregateRoot
{
    private readonly List<Notification> _notifications;
    private readonly List<User> _users;
    public ChatRoom()
    {
        _notifications = new();
        _users = new();
    }

    public ChatRoom(Id id, Name name, CreatedAt createdAt, List<Notification> notifications, List<User> users)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
        _notifications = notifications;
        _users = users;
    }

    public Id Id { get; init; } = null!;
    public Name Name { get; init; } = null!;
    public CreatedAt CreatedAt { get; init; } = CreatedAt.Default;
    public ICollection<User> Users => _users;
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
