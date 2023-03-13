using BlazorChat.Client.Extensions;
using BlazorChat.Client.Models.Feeds.Chat;
using BlazorChat.Client.Models.Feeds.Messages;
using BlazorChat.Client.Models.Requests.ChatRooms;
using BlazorChat.Client.Models.Responses.ChatRooms;
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
    private HubConnection Connection;

    public event Action OnStateChange;
    public event Action OnChatListStateChange;
    public event Action OnMessageListStateChange;

    public bool IsConnected =>
        Connection?.State == HubConnectionState.Connected;
    public List<string> UsersInRoom { get; private set; } = new List<string>();
    public List<Message> Messages { get; private set; } = new List<Message>();
    public List<ChatRoom> ChatRooms { get; private set; } = new List<ChatRoom>();
    public ChatRoom? CurrentChatRoom { get; private set; } = null;

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

    public void ClearMessages()
    {
        Messages = new();
    }

    public void ClearChatUnreadMessages()
    {
        if (CurrentChatRoom != null && CurrentChatRoom.UnreadMessages != 0)
            CurrentChatRoom.UnreadMessages = 0;
        NotifyChatListStateChanged();
    }

    public void InitMessages(List<Message> messages)
    {
        Messages = messages;
        ClearChatUnreadMessages();
        Messages.Where(x => x.Status != "SEEN").ToList().ForEach(async x => await NotifyMessageSeenAsync(x.Id));
    }

    private void NotifyStateChanged() => OnStateChange?.Invoke();
    private void NotifyChatListStateChanged() => OnChatListStateChange?.Invoke();
    private void NotifyMessageListStateChanged() => OnMessageListStateChange?.Invoke();

    private async Task Init()
    {
        Connection = new HubConnectionBuilder()
            .WithUrl(_baseAddress + "chathub")
            .Build();

        InitManager();
        ChatsManager();
        NotificationsManager();
        MessagesManager();

        await Connection.StartAsync();
    }

    private void InitManager()
    {
        Connection.On("InitConnection", async () =>
        {
            await RequestChatListAsync();
            NotifyChatListStateChanged();
        });
    }

    private void ChatsManager()
    {
        Connection.On<ChatListResponse>("ReceiveChatList", (response) =>
        {
            ChatRooms = response.Rooms.ToFeed();
            NotifyChatListStateChanged();
        });

        Connection.On<ChatCreateResponse>("ReceiveCreatedChat", (response) =>
        {
            ChatRooms.Insert(0, response.ToFeed());
            NotifyChatListStateChanged();
        });

        Connection.On<ChatUnreadMessagesResponse>("ReceiveChatUnreadMessages", (response) =>
        {
            var chatRoom = ChatRooms.FirstOrDefault(x => x.Id == response.ChatId);

            if (chatRoom != null && chatRoom != CurrentChatRoom)
            {
                chatRoom.UnreadMessages = response.UnreadMessages;
            }

            NotifyChatListStateChanged();
        });
    }

    private void NotificationsManager()
    {
        Connection.On<string, string>("NotifyUserJoined", async (user, id) =>
        {
            var encodedMsg = $"{user}";
            UsersInRoom.Add(encodedMsg);
            NotifyStateChanged();
            await NotifyCallerJoinedAsync(id);
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
        Connection.On<Message>("ReceiveGroupMessage", async (message) =>
        {
            Messages.Insert(0, message);
            NotifyMessageListStateChanged();
            await NotifyMessageSeenAsync(message.Id);
        });
    }

    private async Task NotifyCallerJoinedAsync(string id)
    {
        if (Connection is not null)
        {
            await Connection.SendAsync("NotifyCallerJoined", id);
        }
    }

    private async Task NotifyMessageSeenAsync(int id)
    {
        if (Connection is not null)
        {
            await Connection.SendAsync("NotifyMessageSeen", id);
        }
    }

    public async Task RequestChatListAsync()
    {
        if (Connection is not null)
        {
            await Connection.SendAsync("RequestChatList");
        }
    }

    public async Task RequestCreateChatAsync(ChatCreateRequest request)
    {
        if (Connection is not null)
        {
            await Connection.SendAsync("RequestCreateChat", request);
        }
    }

    public async Task AddToGroupAsync(string roomId)
    {
        if (Connection is not null)
        {
            await Connection.SendAsync("AddToGroup", roomId);
        }

        CurrentChatRoom = ChatRooms.First(x => x.Id == int.Parse(roomId));
    }

    public async Task RemoveFromGroupAsync(string roomId)
    {
        if (Connection is not null)
        {
            await Connection.SendAsync("RemoveFromGroup", roomId);
        }

        CurrentChatRoom = null;
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
