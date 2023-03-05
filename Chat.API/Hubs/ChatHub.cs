using Microsoft.AspNetCore.SignalR;

namespace Chat.API.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(string message)
    {
        if (Context.User?.Identity?.Name is not null)
        {
            await Clients.All.SendAsync("ReceiveMessage", Context.User.Identity.Name, message);
        }
    }

    public async Task SendPrivateMessage(string user, string message)
    {
        if (Context.User?.Identity?.Name is not null)
        {
            await Clients.User(user).SendAsync("ReceivePrivateMessage", message);
        }
    }

    public async Task SendGroupMessage(string groupName, string message)
    {
        if (Context.User?.Identity?.Name is not null)
        {
            await Clients.Group(groupName).SendAsync("ReceiveGroupMessage", message);
        }
    }

    public async Task AddToGroup(string groupName)
    {
        if (Context.User?.Identity?.Name is not null)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("NotifyUserJoined", $"{Context.User.Identity.Name} has joined the group {groupName}.");
        }
    }

    public async Task RemoveFromGroup(string groupName)
    {
        if (Context.User?.Identity?.Name is not null)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("NotifyUserLeft", $"{Context.User.Identity.Name} has left the group {groupName}.");
        }
    }
}