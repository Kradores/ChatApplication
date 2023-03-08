using Chat.Infrastructure.Enums;

namespace Chat.Domain.Models.Messages.VaulueObjects;

public record MessageStatus
{
    public MessageStatusEnum Value { get; init; }
    private MessageStatus(MessageStatusEnum value) => Value = value;
    public static MessageStatus From(MessageStatusEnum value)
    {
        Validate(value);
        return new(value);
    }

    public static readonly MessageStatus Default = new(MessageStatusEnum.PENDING);

    private static void Validate(MessageStatusEnum value)
    {
    }
}