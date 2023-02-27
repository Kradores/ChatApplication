using Chat.Domain.Models.Authentication.ValueObjects;
using Chat.Domain.Models.ValueObjects;

namespace Chat.Domain.Models.Message.Aggregates;

public class Message : IAggregateRoot
{
    private readonly List<Property> _properties;

    public Message() => _properties = new List<Property>();

    public Id Id { get; init; } = null!;
    public UserId UserId { get; init; } = null!;
    public Id ChatId { get; init; } = null!;
    public CreatedAt CreatedAt { get; init; } = CreatedAt.Default;
    public ICollection<Property> Properties => _properties;

    public void Add(Property property)
    {
        _properties.Add(property);
    }

    public void Remove(Property property)
    {
        _properties.Remove(property);
    }

}
