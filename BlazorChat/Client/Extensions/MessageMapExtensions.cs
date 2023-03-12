using BlazorChat.Client.Models.Feeds.Messages;
using BlazorChat.Client.Models.Requests.Messages;
using BlazorChat.Client.Models.Responses.Messages;

namespace BlazorChat.Client.Extensions;

public static class MessageMapExtensions
{
    public static Dictionary<string, string> ToDictionary(this MessagesRequest request)
    {
        var query = new Dictionary<string, string>();

        query.Add(nameof(request.ChatId), request.ChatId.ToString()); 
        query.Add(nameof(request.PageNumber), request.PageNumber.ToString()); 
        query.Add(nameof(request.PageSize), request.PageSize.ToString());

        return query;
    }

    public static List<Message> ToFeed(this MessagesResponse response)
    {
        return response.Messages.Select(x => new Message()
        {
            Id = x.Id,
            ChatRoomId = x.ChatRoomId,
            Text = x.Text,
            CreatedAt = x.CreatedAt,
            SenderName = x.SenderName
        }).ToList();
    }
}
