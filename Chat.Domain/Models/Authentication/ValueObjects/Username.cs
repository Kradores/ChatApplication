namespace Chat.Domain.Models.Authentication.ValueObjects;

public record Username
{
    public string Value { get; init; }
    private Username(string value) => Value = value;
    public static Username From(string value)
    {
        Validate(value);
        return new(Hash(value));
    }

    private static void Validate(string value)
    {
    }

    private static string Hash(string value)
    {
        return value;
    }
}