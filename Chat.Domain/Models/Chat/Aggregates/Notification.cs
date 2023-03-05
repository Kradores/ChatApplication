using Chat.Domain.Models.Authentication.ValueObjects;
using Chat.Domain.Models.ValueObjects;

namespace Chat.Domain.Models.Chat.Aggregates;

public class Notification
{
    public Notification() { }

    public Notification(Id id, UnreadMessagesCount unreadMessagesCount, UserId userId)
    {
        Id = id;
        UnreadMessagesCount = unreadMessagesCount;
        UserId = userId;
    }

    public Id Id { get; init; } = null!;
    public UnreadMessagesCount UnreadMessagesCount { get; set; } = UnreadMessagesCount.Default;
    public UserId UserId { get; init; } = null!;
}
