namespace Chat.API.Hubs.SendArguments;

public class MessageArg
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
}
