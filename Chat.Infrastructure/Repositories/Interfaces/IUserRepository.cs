using Chat.Infrastructure.Entities;

namespace Chat.Infrastructure.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserAsync(string id, CancellationToken cancellationToken);
}
