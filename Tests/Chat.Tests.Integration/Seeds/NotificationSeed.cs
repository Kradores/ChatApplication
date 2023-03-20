using Chat.Infrastructure.Entities;

namespace Chat.Tests.Integration.Seeds;

public static class NotificationSeed
{
    public static readonly List<Notification> Notifications = new List<Notification>()
    {
        new Notification()
        {
            ChatRoomId = ChatSeeds.Chats[0].Id,
            UserId = UserSeeds.Users[0].Id,
            UnreadMessages = 3
        },
        new Notification()
        {
            ChatRoomId = ChatSeeds.Chats[0].Id,
            UserId = UserSeeds.Users[1].Id,
            UnreadMessages = 4
        }
    };
}
