using Microsoft.AspNetCore.Identity;

namespace Chat.Infrastructure.Entities;

public class User : IdentityUser
{
    public ICollection<ChatRoom> ChatRooms { get; init; } = new List<ChatRoom>();
}
