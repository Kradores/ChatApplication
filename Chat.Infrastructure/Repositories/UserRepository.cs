using Chat.Infrastructure.Entities;

namespace Chat.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ChatContext _context;
    public UserRepository(ChatContext context) =>
        _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<User?> GetUserAsync(string id, CancellationToken cancellationToken)
    {
        return await _context.Users.FindAsync(id, cancellationToken);
    }
}
