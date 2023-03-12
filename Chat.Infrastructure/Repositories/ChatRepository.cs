using Chat.Infrastructure.Entities;
using Chat.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Chat.Infrastructure.Repositories;

public class ChatRepository : IChatRepository
{
    private readonly ChatContext _context;
    public ChatRepository(ChatContext context) =>
        _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task CreateAsync(ChatRoom room, CancellationToken cancellationToken)
    {
        _context.ChatRooms.Update(room);
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

    public async Task<ChatRoom> AttachUsersAsync(ChatRoom chatRoom)
    {
        await _context.Entry(chatRoom)
            .Collection(i => i.Users)
            .LoadAsync();

        return chatRoom;
    }

    public async Task<ChatRoom> AttachNotificationsAsync(ChatRoom chatRoom)
    {
        await _context.Entry(chatRoom)
            .Collection(i => i.Notifications)
            .LoadAsync();

        return chatRoom;
    }

    public async Task<ChatRoom?> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.ChatRooms.Where(x => x.Id == id).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<List<ChatRoom>> GetByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        return await _context.ChatRooms
            .Where(x => x.Users.Any(y => y.Id == userId))
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
