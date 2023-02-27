namespace Chat.Domain.Models.ValueObjects;

public record Name
{
    public string Value { get; init; }
    private Name(string value) => Value = value;
    public static Name From(string value)
    {
        Validate(value);
        return new(value);
    }

    private static void Validate(string value)
    {
    }
}
