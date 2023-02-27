namespace Chat.Infrastructure.Entities;

public class UserReference
{
    public string UserId { get; set; } = null!;
    public int ChatRoomId { get; set; }
}
