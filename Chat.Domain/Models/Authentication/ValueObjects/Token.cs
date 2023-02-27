namespace Chat.Domain.Models.Authentication.ValueObjects;

public record Token
{
    public string Value { get; init; }
    private Token(string value) => Value = value;
    public static Token From(string value)
    {
        Validate(value);
        return new(value);
    }

    private static void Validate(string value)
    {
    }
}