using BlazorChat.Client.Models.Feeds.Chat;

namespace BlazorChat.Client.StateContainers;

public class ChatRoomsStateContainer
{
    public List<ChatRoom> Value { get; set; } = new();
    public event Action OnStateChange;

    public void Add(ChatRoom chatRoom)
    {
        Value.Add(chatRoom);
        NotifyStateChanged();
    }

    public void AddFirst(ChatRoom chatRoom)
    {
        Value.Insert(0, chatRoom);
        NotifyStateChanged();
    }

    public void Init(List<ChatRoom> chatRooms)
    {
        Value = chatRooms;
        NotifyStateChanged();
    }

    public void Remove(ChatRoom chatRoom)
    {
        Value.Remove(chatRoom);
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnStateChange?.Invoke();
}
