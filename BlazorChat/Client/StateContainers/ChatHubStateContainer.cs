using BlazorChat.Client.Pages;
using BlazorChat.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorChat.Client.StateContainers;

public class ChatHubStateContainer : IAsyncDisposable
{
    private readonly CustomAuthenticationStateProvider _stateProvider;
    private readonly string _baseAddress;
    public HubConnection Connection { get; private set; }
    public event Action OnStateChange;
    public bool IsConnected =>
        Connection?.State == HubConnectionState.Connected;
    public List<string> UsersInRoom { get; private set; } = new List<string>();
    public List<string> Messages { get; private set; } = new List<string>();

    public ChatHubStateContainer(AuthenticationStateProvider stateProvider, IWebAssemblyHostEnvironment environment)
    {
        _stateProvider = (CustomAuthenticationStateProvider)stateProvider;
        _stateProvider.AuthenticationStateChanged += AuthenticationStateChangedHandler;
        _baseAddress = environment.BaseAddress;
    }

    private void AuthenticationStateChangedHandler(Task<AuthenticationState> task)
    {
        if (task is not null)
        {
            var authState = task.Result;
            var identity = authState.User.Identity;

            if (identity?.IsAuthenticated != null && identity?.IsAuthenticated == true)
            {
                Init().ConfigureAwait(true);
            }
        }
    }

    private void ClearUsers()
    {
        UsersInRoom = new();
    }

    private void ClearMessages()
    {
        Messages = new();
    }

    private void NotifyStateChanged() => OnStateChange?.Invoke();

    private async Task Init()
    {
        Connection = new HubConnectionBuilder()
            .WithUrl(_baseAddress + "chathub")
            .Build();

        NotificationsManager();
        MessagesManager();

        await Connection.StartAsync();
    }

    private void NotificationsManager()
    {
        Connection.On<string, string>("NotifyUserJoined", async (user, id) =>
        {
            var encodedMsg = $"{user}";
            UsersInRoom.Add(encodedMsg);
            await NotifyCallerJoinedAsync(id);
            NotifyStateChanged();
        });

        Connection.On<string>("NotifyCallerJoined", (user) =>
        {
            var encodedMsg = $"{user}";
            UsersInRoom.Add(encodedMsg);
            NotifyStateChanged();
        });

        Connection.On<string>("NotifyUserLeft", (user) =>
        {
            var encodedMsg = $"{user}";
            UsersInRoom.Remove(encodedMsg);
            NotifyStateChanged();
        });

        // notify chat was created
    }

    private void MessagesManager()
    {
        Connection.On<string, string>("ReceiveGroupMessage", (user, message) =>
        {
            var encodedMsg = $"{user}: {message}";
            Messages.Add(encodedMsg);
            NotifyStateChanged();
        });
    }

    private async Task NotifyCallerJoinedAsync(string id)
    {
        if (Connection is not null)
        {
            await Connection.SendAsync("NotifyCallerJoined", id);
        }
    }

    public async Task AddToGroupAsync(string roomId)
    {
        if (Connection is not null)
        {
            await Connection.SendAsync("AddToGroup", roomId);
        }
    }

    public async Task RemoveFromGroupAsync(string roomId)
    {
        if (Connection is not null)
        {
            await Connection.SendAsync("RemoveFromGroup", roomId);
        }

        ClearUsers();
        ClearMessages();
    }

    public async Task SendGroupMessageAsync(string roomId, string message)
    {
        if (Connection is not null)
        {
            await Connection.SendAsync("SendGroupMessage", roomId, message);
        }
    }

    public async ValueTask DisposeAsync()
    {
        _stateProvider.AuthenticationStateChanged -= AuthenticationStateChangedHandler;

        if (Connection is not null)
        {
            await Connection.DisposeAsync();
        }
    }
}
