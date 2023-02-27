using Chat.Domain.Models.Authentication.ValueObjects;
using Chat.Domain.Models.Message.VaulueObjects;
using Chat.Domain.Models.ValueObjects;

namespace Chat.Domain.Models.Message.Aggregates;

public class Property
{
    public Id Id { get; init; } = null!;
    public UserId SenderId { get; init; } = null!;
    public UserId ReceiverId { get; init; } = null!;
    public Id ChatId { get; init; } = null!;
    public MessageStatus Status { get; init; } = MessageStatus.Default;

}
