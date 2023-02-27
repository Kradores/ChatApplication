using Chat.Domain.Models.ValueObjects;

namespace Chat.Domain.Models.Chat.Aggregates;

public class Notification
{
    public Notification() { }

    public Id Id { get; init; } = null!;
    public UnreadMessagesCount UnreadMessagesCount { get; set; } = UnreadMessagesCount.Default;
    public Id UserId { get; init; } = null!;
}
