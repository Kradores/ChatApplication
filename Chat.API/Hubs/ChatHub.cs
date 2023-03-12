using Chat.Domain.Factories.Interfaces;
using Chat.Domain.Models.Authentication.ValueObjects;
using Chat.Domain.Models.Messages.VaulueObjects;
using Chat.Domain.Models.ValueObjects;
using Chat.Infrastructure.Enums;
using Microsoft.AspNetCore.SignalR;

namespace Chat.API.Hubs;

public class ChatHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        if (Context.UserIdentifier is not null)
        {
            await Clients.User(Context.UserIdentifier).SendAsync("InitConnection");
        }

        await base.OnConnectedAsync();
    }

    public async Task RequestChatList(IChatFactory chatFactory)
    {
        if (Context.UserIdentifier is not null)
        {
            var chats = await chatFactory.GetAsync(UserId.From(Context.UserIdentifier), default);
            var response = new Endpoints.ChatRoom.GetMany.Response()
            {
                Rooms = chats.Select(x => new Endpoints.ChatRoom.GetMany.Response.Room()
                {
                    Id = x.Id.Value,
                    Name = x.Name.Value,
                    UnreadMessages = x.Notifications.FirstOrDefault(y => y.UserId.Value == Context.UserIdentifier)?.UnreadMessagesCount.Value ?? 0
                }).ToList()
            };

            await Clients.User(Context.UserIdentifier).SendAsync("ReceiveChatList", response);
        }
    }

    public async Task RequestCreateChat(Endpoints.ChatRoom.Create.Request request, IChatFactory chatFactory)
    {
        if (Context.UserIdentifier is not null)
        {
            var room = await chatFactory.CreateAsync(Name.From(request.Name), request.UserIds.Select(UserId.From), default);
            var response = new Endpoints.ChatRoom.Create.Response()
            {
                Id = room.Id.Value,
                Name = room.Name.Value,
                UnreadMessages = 0
            };

            foreach (var user in  room.Users)
            {
                await Clients.User(user.Id.Value).SendAsync("ReceiveCreatedChat", response);
            }
        }
    }

    public async Task SendPrivateMessage(string user, string message)
    {
        if (Context.User?.Identity?.Name is not null)
        {
            await Clients.User(user).SendAsync("ReceivePrivateMessage", Context.User.Identity.Name, message);
        }
    }

    public async Task SendGroupMessage(string roomId, string text, IMessageFactory messageFactory)
    {
        if (Context.User?.Identity?.Name is not null && Context.UserIdentifier is not null)
        {
            var message = await messageFactory.CreateAsync(
                UserId.From(Context.UserIdentifier),
                Id.From(int.Parse(roomId)),
                Text.From(text), default);

            await Clients.Group(roomId).SendAsync("ReceiveGroupMessage", new Endpoints.Messages.GetMany.Response.Message()
            {
                Id = message.Id.Value,
                ChatRoomId = message.Id.Value,
                Text = message.Text.Value,
                CreatedAt = message.CreatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                SenderName = message.User.Username.Value
            });
        }
    }

    public async Task NotifyMessageSeen(int messageId, IMessageFactory messageFactory)
    {
        if (Context.UserIdentifier is not null)
        {
            await messageFactory.UpdateAsync(
                UserId.From(Context.UserIdentifier),
                Id.From(messageId),
                MessageStatus.From(MessageStatusEnum.SEEN), default);
        }
    }

    public async Task AddToGroup(string roomId)
    {
        if (Context.User?.Identity?.Name is not null)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            await Clients.Group(roomId).SendAsync("NotifyUserJoined", Context.User.Identity.Name, Context.UserIdentifier);
        }
    }

    public async Task NotifyCallerJoined(string callerId)
    {
        if (Context.User?.Identity?.Name is not null && Context.UserIdentifier != callerId)
        {
            await Clients.User(callerId).SendAsync("NotifyCallerJoined", Context.User.Identity.Name);
        }
    }

    public async Task RemoveFromGroup(string groupName)
    {
        if (Context.User?.Identity?.Name is not null)
        {
            await Clients.Group(groupName).SendAsync("NotifyUserLeft", Context.User.Identity.Name);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}