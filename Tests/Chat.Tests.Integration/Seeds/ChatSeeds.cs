using Chat.Infrastructure.Entities;

namespace Chat.Tests.Integration.Seeds;

public static class ChatSeeds
{
    public static readonly List<ChatRoom> Chats = new()
    {
        new ChatRoom()
        {
            Name = "ViorelAndViorica",
            CreatedAt = DateTime.Now,
            Notifications = new List<Notification>()
            {
                new Notification()
                {
                    UserId = UserSeeds.Users[0].Id,
                    UnreadMessages = 3
                },
                new Notification()
                {
                    UserId = UserSeeds.Users[1].Id,
                    UnreadMessages = 4
                }
            },
            Users = new List<User>()
            {
                UserSeeds.Users[0],
                UserSeeds.Users[1]
            }
        },
        new ChatRoom()
        {
            Name = "ViorelAndMarcel",
            CreatedAt = DateTime.Now,
            Notifications = new List<Notification>()
            {
                new Notification()
                {
                    UserId = UserSeeds.Users[0].Id,
                    UnreadMessages = 5
                },
                new Notification()
                {
                    UserId = UserSeeds.Users[2].Id,
                    UnreadMessages = 6
                }
            },
            Users = new List<User>()
            {
                UserSeeds.Users[0],
                UserSeeds.Users[2]
            }
        },
        new ChatRoom()
        {
            Name = "VVM",
            CreatedAt = DateTime.Now,
            Notifications = new List<Notification>()
            {
                new Notification()
                {
                    UserId = UserSeeds.Users[0].Id,
                    UnreadMessages = 7
                },
                new Notification()
                {
                    UserId = UserSeeds.Users[1].Id,
                    UnreadMessages = 8
                },
                new Notification()
                {
                    UserId = UserSeeds.Users[2].Id,
                    UnreadMessages = 9
                }
            },
            Users = new List<User>()
            {
                UserSeeds.Users[0],
                UserSeeds.Users[1],
                UserSeeds.Users[2],
            }
        },
    };
}
