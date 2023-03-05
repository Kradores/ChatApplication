using Chat.Domain.Models.Authentication.ValueObjects;
using Chat.Domain.Models.Chat.Aggregates;
using Chat.Domain.Models.ValueObjects;
using ChatRoomEntity = Chat.Infrastructure.Entities.ChatRoom;

namespace Chat.Domain.Models.Chat;

public static class MapExtentions
{
    public static ChatRoom ToModel(this ChatRoomEntity entity)
    {
        var notifications = entity.Notifications.Select(x => new Notification(Id.From(x.Id),
            UnreadMessagesCount.From(x.UnreadMessages),
            UserId.From(x.UserId))).ToList();

        return new(
            Id.From(entity.Id),
            Name.From(entity.Name),
            CreatedAt.From(entity.CreatedAt),
            notifications);
    }
}
