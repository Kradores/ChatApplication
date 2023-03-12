using Chat.Domain.Models.Authentication.ValueObjects;
using Chat.Domain.Models.Messages.Aggregates;
using Chat.Domain.Models.Messages.VaulueObjects;
using Chat.Domain.Models.ValueObjects;
using MessagePropertyEntity = Chat.Infrastructure.Entities.MessageProperty;
using MessageEntity = Chat.Infrastructure.Entities.Message;
using Chat.Domain.Models.Authentication;

namespace Chat.Domain.Models.Messages;

public static class MapExtensions
{
    public static Message ToModel(this MessageEntity entity)
    {
        var properties = entity.Properties.Select(x => new Property()
        {
            Id = Id.From(x.Id),
            SenderId = UserId.From(x.SenderId),
            ReceiverId = UserId.From(x.ReceiverId),
            ChatId = Id.From(x.ChatRoomId),
            Status = MessageStatus.From(x.Status)
        }).ToList();

        return new(Id.From(entity.Id),
            UserId.From(entity.UserId),
            entity.User.ToModel(),
            Id.From(entity.ChatRoomId),
            Text.From(entity.Data),
            CreatedAt.From(entity.CreatedAt),
            entity.Properties.ToModel());
    }

    public static List<Message> ToModel(this List<MessageEntity> entities)
    {
        return entities.Select(x => x.ToModel()).ToList();
    }

    public static Property ToModel(this MessagePropertyEntity entity)
    {
        return new Property()
        {
            Id = Id.From(entity.Id),
            SenderId = UserId.From(entity.SenderId),
            ReceiverId = UserId.From(entity.ReceiverId),
            ChatId = Id.From(entity.ChatRoomId),
            Status = MessageStatus.From(entity.Status)
        };
    }

    public static List<Property> ToModel(this ICollection<MessagePropertyEntity> entities)
    {
        return entities.Select(x => x.ToModel()).ToList();
    }

    public static MessageEntity ToEntity(this Message model)
    {
        return new()
        {
            Id = model.Id.Value,
            UserId = model.UserId.Value,
            ChatRoomId = model.ChatId.Value,
            Data = model.Text.Value,
            CreatedAt = model.CreatedAt.Value,
            Properties = model.Properties.ToEntity()
        };
    }

    public static MessagePropertyEntity ToEntity(this Property model)
    {
        return new()
        {
            Id = model.Id.Value,
            SenderId = model.SenderId.Value,
            ReceiverId = model.ReceiverId.Value,
            ChatRoomId = model.ChatId.Value,
            Status = model.Status.Value,
        };
    }

    public static ICollection<MessagePropertyEntity> ToEntity(this ICollection<Property> models)
    {
        return models.Select(x => x.ToEntity()).ToList();
    }
}
