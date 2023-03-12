namespace Chat.API.Endpoints.Messages.GetMany;

public class Request
{
    public int ChatId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public static ValueTask<Request> BindAsync(HttpContext context) => ValueTask.FromResult<Request>(new()
    {
        PageNumber = !string.IsNullOrEmpty(context.Request.Query["pageNumber"]) ? int.Parse(context.Request.Query["pageNumber"]) : 1,
        PageSize = !string.IsNullOrEmpty(context.Request.Query["pageSize"]) ? int.Parse(context.Request.Query["pageSize"]) : 20,
        ChatId = int.Parse(context.Request.Query["chatId"])
    });
}
