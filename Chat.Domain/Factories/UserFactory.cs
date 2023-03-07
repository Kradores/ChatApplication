using Chat.Domain.Factories.Interfaces;
using Chat.Domain.Models.Authentication;
using Chat.Domain.Models.Authentication.Aggregates;
using Chat.Infrastructure.Repositories.Interfaces;

namespace Chat.Domain.Factories;

public class UserFactory : IUserFactory
{
    private readonly IUserRepository _userRepository;

    public UserFactory(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<User>> GetAsync(CancellationToken cancellationToken)
    {
        var userEntities = await _userRepository.GetAsync(cancellationToken);

        return userEntities.Select(x => x.ToModel()).ToList();
    }
}
