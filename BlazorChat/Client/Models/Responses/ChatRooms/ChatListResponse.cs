namespace BlazorChat.Client.Models.Responses.ChatRooms;

public class ChatListResponse
{
    public List<ChatRoom> Rooms { get; set; } = new List<ChatRoom>();

    public class ChatRoom
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
