using Chat.Infrastructure.Entities;
using Chat.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Repositories;

public class ChatRepository : IChatRepository
{
    private readonly ChatContext _context;
    public ChatRepository(ChatContext context) =>
        _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task CreateAsync(ChatRoom room, CancellationToken cancellationToken)
    {
        await _context.ChatRooms.AddAsync(room);
        await _context.SaveChangesAsync();
    }

    public Task DeleteAsync(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ChatRoom?> GetAsync(string name, CancellationToken cancellationToken)
    {
        return await _context.ChatRooms.Where(x => x.Name == name).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<List<ChatRoom>> GetByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        return await _context.ChatRooms
            .Where(x => x.Users.Any(y => y.Id == userId))
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public Task UpdateAsync(ChatRoom room, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
