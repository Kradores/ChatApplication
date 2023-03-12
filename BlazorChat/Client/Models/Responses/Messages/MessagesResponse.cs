namespace BlazorChat.Client.Models.Responses.Messages;

public class MessagesResponse
{
    public List<Message> Messages { get; set; } = new List<Message>();

    public class Message
    {
        public int Id { get; set; }
        public int ChatRoomId { get; set; }
        public string Text { get; set; } = null!;
        public string CreatedAt { get; set; } = null!;
        public string SenderName { get; set; } = null!;
    }
}
