using BlazorChat.Client.Models.Feeds.Chat;
using BlazorChat.Client.Models.Responses.Users;

namespace BlazorChat.Client.Extensions;

public static class UserMapExtensions
{
    public static ChatCreateInput.UserCheckbox ToFeed(this UsersListResponse.User user)
    {
        return new ChatCreateInput.UserCheckbox()
        {
            Id = user.Id,
            Name = user.Name,
            IsChecked = false
        };
    }

    public static List<ChatCreateInput.UserCheckbox> ToFeed(this List<UsersListResponse.User> users)
    {
        return users.Select(user => user.ToFeed()).ToList();
    }
}
