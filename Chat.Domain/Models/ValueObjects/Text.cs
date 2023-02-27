namespace Chat.Domain.Models.ValueObjects;

public record Text
{
    public string Value { get; init; }
    private Text(string value) => Value = value;
    public static Text From(string value)
    {
        Validate(value);
        return new(value);
    }

    private static void Validate(string value)
    {
    }
}