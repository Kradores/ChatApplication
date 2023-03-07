using Chat.Domain.Models.Authentication.Aggregates;

namespace Chat.Domain.Factories.Interfaces;

public interface IUserFactory
{
    Task<List<User>> GetAsync(CancellationToken cancellationToken);
}
