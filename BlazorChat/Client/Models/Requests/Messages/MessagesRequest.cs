namespace BlazorChat.Client.Models.Requests.Messages;

public class MessagesRequest
{
    public int ChatId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
