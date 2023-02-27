namespace Chat.Domain.Models.ValueObjects;

public record CreatedAt
{
    public DateTime Value { get; init; }
    private CreatedAt(DateTime value) => Value = value;
    public static CreatedAt From(DateTime value)
    {
        Validate(value);
        return new(value);
    }

    public static readonly CreatedAt Default = new(DateTime.Now);

    private static void Validate(DateTime value)
    {
    }
}