using Chat.Infrastructure.Entities;

namespace Chat.Infrastructure.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserAsync(string id, CancellationToken cancellationToken);
}
