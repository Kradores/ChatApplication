using Chat.API.Hubs.SendArguments;
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
            var response = chats.Select(x => new ChatArg()
            {
                Id = x.Id.Value,
                Name = x.Name.Value
            }).ToList();

            await Clients.User(Context.UserIdentifier).SendAsync("ReceiveChatList", response);
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

            await Clients.Group(roomId).SendAsync("ReceiveGroupMessage", Context.User.Identity.Name, new MessageArg()
            {
                Id = message.Id.Value,
                Text = message.Text.Value
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