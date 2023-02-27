using Microsoft.AspNetCore.Identity;

namespace Chat.Infrastructure.Entities;

public class User : IdentityUser
{
    //public int Id { get; set; }
    //public string Username { get; set; } = null!;
    //public string Password { get; set; } = null!;
    public ICollection<ChatRoom> ChatRooms { get; init; } = new List<ChatRoom>();
    //public Bearer? Bearer { get; init; } = null!;
}
