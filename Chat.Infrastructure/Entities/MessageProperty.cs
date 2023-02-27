using Chat.Infrastructure.Enums;

namespace Chat.Infrastructure.Entities;

public class MessageProperty
{
    public int Id { get; set; }
    public string SenderId { get; set; } = null!;
    public string ReceiverId { get; set; } = null!;
    public int ChatRoomId { get; set; }
    public int MessageId { get; set; }
    public MessageStatusEnum Status { get; set; } = MessageStatusEnum.PENDING;
}
