using Chat.Domain.Models.ValueObjects;

namespace Chat.Domain.Models.Chat.Aggregates;

public class Chat : IAggregateRoot
{
    private readonly List<Notification> _notifications;
    public Chat() => _notifications = new();

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
