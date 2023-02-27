namespace Chat.Domain.Models.ValueObjects;

public record UnreadMessagesCount
{
    public int Value { get; init; }
    private UnreadMessagesCount(int value) => Value = value;
    public static UnreadMessagesCount From(int value)
    {
        Validate(value);
        return new(value);
    }

    public static readonly UnreadMessagesCount Default = new(0);

    private static void Validate(int value)
    {
    }
}