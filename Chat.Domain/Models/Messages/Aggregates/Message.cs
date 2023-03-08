using Chat.Domain.Models.Authentication.ValueObjects;
using Chat.Domain.Models.Messages.VaulueObjects;
using Chat.Domain.Models.ValueObjects;

namespace Chat.Domain.Models.Messages.Aggregates;

public class Message : IAggregateRoot
{
    private readonly List<Property> _properties;

    public Message() => _properties = new List<Property>();

    public Message(Id id, UserId userId, Id chatId, Text text, CreatedAt createdAt, List<Property> properties)
    {
        Id = id;
        UserId = userId;
        ChatId = chatId;
        Text = text;
        CreatedAt = createdAt;
        _properties = properties;
    }

    public Id Id { get; init; } = null!;
    public UserId UserId { get; init; } = null!;
    public Id ChatId { get; init; } = null!;
    public Text Text { get; init; } = null!;
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

    public void UpdatePropertyStatus(UserId receiverId, MessageStatus status)
    {
        var property = _properties.Where(x => x.ReceiverId == receiverId).First();

        property.Status = status;
    }

}
