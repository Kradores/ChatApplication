namespace Chat.API.Endpoints.ChatRoom.Create;

public class Response
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int UnreadMessages { get; set; }
}
