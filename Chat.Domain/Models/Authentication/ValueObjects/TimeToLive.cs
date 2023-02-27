using Chat.Domain.Models.ValueObjects;

namespace Chat.Domain.Models.Authentication.ValueObjects;

public record TimeToLive
{
    public int Value { get; init; }
    private TimeToLive(int value) => Value = value;
    public static TimeToLive From(int value)
    {
        Validate(value);
        return new(value);
    }

    private static void Validate(int value)
    {
    }

    public static readonly TimeToLive Default = new(120);
}