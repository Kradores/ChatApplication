namespace Chat.Infrastructure.Entities;

public class Bearer
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Token { get; set; } = null!;
    public int TimeToLive { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
