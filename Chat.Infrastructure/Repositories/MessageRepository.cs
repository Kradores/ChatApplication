using Chat.Infrastructure.Entities;
using Chat.Infrastructure.Filters;
using Chat.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly ChatContext _context;
    public MessageRepository(ChatContext context) =>
        _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<Message> AttachPropertiesAsync(Message message)
    {
        await _context.Entry(message)
            .Collection(x => x.Properties)
            .LoadAsync();

        return message;
    }

    public async Task CreateAsync(Message message)
    {
        _context.Messages.Update(message);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Message>> GetAsync(int chatId, Pagination pagination, CancellationToken cancellationToken)
    {
        return await _context.Messages
            .AsNoTracking()
            .Where(x => x.ChatRoomId == chatId)
            .OrderByDescending(x => x.CreatedAt)
            .Skip(pagination.Skip)
            .Take(pagination.Take)
            .ToListAsync(cancellationToken);
    }

    public async Task<Message?> GetAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _context.Messages.Where(x => x.Id == id).SingleOrDefaultAsync(cancellationToken);

        return entity;
    }

    public async Task UpdateAsync(Message message)
    {
        await _context.SaveChangesAsync();
    }
}
