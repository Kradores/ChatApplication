using Chat.Domain.Models.Authentication.Aggregates;
using Microsoft.AspNetCore.Identity;

namespace Chat.Domain.Models.Authentication.ValueObjects;

public record Password
{
    public string Value { get; init; }
    private Password(string value) => Value = value;
    public static Password From(string value)
    {
        Validate(value);
        return new(value);
    }

    private static void Validate(string value)
    {
    }
}