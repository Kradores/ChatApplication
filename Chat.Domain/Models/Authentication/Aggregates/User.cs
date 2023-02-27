using Chat.Domain.Models.Authentication.ValueObjects;
using Chat.Domain.Models.ValueObjects;

namespace Chat.Domain.Models.Authentication.Aggregates;

public class User : IAggregateRoot
{
    public User() { }

    public UserId Id { get; init; } = null!;
    public Username Username { get; init; } = null!;
    public Password Password { get; init; } = null!;
    public CreatedAt CreatedAt { get; init; } = CreatedAt.Default;
}
