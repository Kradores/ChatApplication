using Chat.Domain.Models.Authentication.ValueObjects;
using Chat.Domain.Models.Chat.Aggregates;
using Chat.Domain.Models.ValueObjects;

namespace Chat.Domain.Factories.Interfaces;

public interface IChatFactory
{
    Task<ChatRoom> CreateAsync(Name name, IEnumerable<UserId> userIds, CancellationToken cancellationToken);
    Task<ChatRoom> UpdateAsync(ChatRoom room, CancellationToken cancellationToken);
    Task<ChatRoom?> ResetUnreadMessagesAsync(Id chatId, UserId userId, CancellationToken cancellationToken);
    Task DeleteAsync(Name name, CancellationToken cancellationToken);
    Task<ChatRoom?> GetAsync(Name name, CancellationToken cancellationToken);
    Task<ChatRoom?> GetAsync(Id chatId, CancellationToken cancellationToken);
    Task<List<ChatRoom>> GetAsync(UserId userId, CancellationToken cancellationToken);
}
