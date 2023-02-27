namespace Chat.Domain.Models.ValueObjects;

public record Id
{
    public int Value { get; init; }
    private Id(int value) => Value = value;
    public static Id From(int value)
    {
        Validate(value);
        return new(value);
    }

    private static void Validate(int value)
    {
    }
}
