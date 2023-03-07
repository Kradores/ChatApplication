using BlazorChat.Client.Models.Feeds.Chat;
using BlazorChat.Client.Models.Requests.ChatRooms;
using BlazorChat.Client.Models.Responses.ChatRooms;

namespace BlazorChat.Client.Extensions;

public static class ChatMapExtentions
{
    public static ChatRoom ToFeed(this ChatCreateResponse chat)
    {
        return new()
        {
            Id = chat.Id,
            Name = chat.Name,
        };
    }

    public static ChatRoom ToFeed(this ChatListResponse.ChatRoom chat)
    {
        return new()
        {
            Id = chat.Id,
            Name = chat.Name,
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
            UserIds = input.UserCheckboxes.Select(x => x.Id).ToList(),
        };
    }
}
