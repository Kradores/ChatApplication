using Chat.Infrastructure.Entities;
using Chat.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ChatContext _context;
    public UserRepository(ChatContext context) =>
        _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<User?> GetAsync(string id, CancellationToken cancellationToken)
    {
        return await _context.Users.FindAsync(id, cancellationToken);
    }

    public async Task<List<User>> GetAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
    {
        return await _context.Users.Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
    }
}
