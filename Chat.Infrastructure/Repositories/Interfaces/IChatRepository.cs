using Chat.Infrastructure.Entities;

namespace Chat.Infrastructure.Repositories.Interfaces;

public interface IChatRepository
{
    Task CreateAsync(ChatRoom room, CancellationToken cancellationToken);
    Task UpdateAsync(ChatRoom room, CancellationToken cancellationToken);
    Task DeleteAsync(string name, CancellationToken cancellationToken);
    Task<ChatRoom?> GetAsync(string name, CancellationToken cancellationToken);
    Task<ChatRoom?> GetAsync(int id, CancellationToken cancellationToken);
    Task<List<ChatRoom>> GetByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task<ChatRoom> AttachUsersAsync(ChatRoom chatRoom);
}
