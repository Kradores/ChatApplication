using Chat.Infrastructure.Entities;

namespace Chat.Infrastructure.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetAsync(string id, CancellationToken cancellationToken);
    Task<List<User>> GetAsync(IEnumerable<string> ids, CancellationToken cancellationToken);
}
