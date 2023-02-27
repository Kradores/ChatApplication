using Chat.Domain.Enums;

namespace Chat.Domain.Models.Message.VaulueObjects;

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