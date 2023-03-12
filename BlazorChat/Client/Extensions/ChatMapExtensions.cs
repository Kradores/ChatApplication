using BlazorChat.Client.Models.Feeds.Chat;
using BlazorChat.Client.Models.Requests.ChatRooms;
using BlazorChat.Client.Models.Responses.ChatRooms;

namespace BlazorChat.Client.Extensions;

public static class ChatMapExtensions
{
    public static ChatRoom ToFeed(this ChatCreateResponse chat)
    {
        return new()
        {
            Id = chat.Id,
            Name = chat.Name,
            UnreadMessages = chat.UnreadMessages,
        };
    }

    public static ChatRoom ToFeed(this ChatListResponse.ChatRoom chat)
    {
        return new()
        {
            Id = chat.Id,
            Name = chat.Name,
            UnreadMessages = chat.UnreadMessages,
        };
    }

    public static List<ChatRoom> ToFeed(this List<ChatListResponse.ChatRoom> chats)
    {
        return chats.Select(x => x.ToFeed()).ToList();
    }

    public static ChatCreateRequest ToRequest(this ChatCreateInput input)
    {
        return new()
        {
            Name = input.Name,
            UserIds = input.UserCheckboxes.Where(x => x.IsChecked).Select(x => x.Id).ToList(),
        };
    }
}
