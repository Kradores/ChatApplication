using Chat.Domain.Factories.Interfaces;
using Chat.Domain.Models.ValueObjects;
using ChatRoomEntity = Chat.Infrastructure.Entities.ChatRoom;
using Chat.Infrastructure.Repositories.Interfaces;
using Chat.Domain.Models.Chat.Aggregates;
using Chat.Domain.Models.Authentication.ValueObjects;
using UserEntity = Chat.Infrastructure.Entities.User;
using NotificationEntity = Chat.Infrastructure.Entities.Notification;
using Chat.Domain.Models.Chat;

namespace Chat.Domain.Factories;

public class ChatFactory : IChatFactory
{
    private readonly IChatRepository _chatRepository;
    private readonly IUserRepository _userRepository;

    public ChatFactory(IChatRepository chatRepository, IUserRepository userRepository)
    {
        _chatRepository = chatRepository;
        _userRepository = userRepository;
    }

    public async Task<ChatRoom> CreateAsync(Name name, IEnumerable<UserId> userIds, CancellationToken cancellationToken)
    {
        List<UserEntity> users = await _userRepository.GetAsync(userIds.Select(x => x.Value), cancellationToken);
        List<NotificationEntity> notifications = users.Select(x => new NotificationEntity()
        {
            UserId = x.Id
        }).ToList();

        ChatRoomEntity entity = new()
        {
            Name = name.Value,
            Users = users,
            Notifications = notifications
        };

        await _chatRepository.CreateAsync(entity, cancellationToken);

        return entity.ToModel();
    }

    public Task DeleteAsync(Name name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ChatRoom?> GetAsync(Name name, CancellationToken cancellationToken)
    {
        var entity = await _chatRepository.GetAsync(name.Value, cancellationToken);

        if (entity == null) return null;

        return entity.ToModel();
    }

    public async Task<List<ChatRoom>> GetAsync(UserId userId, CancellationToken cancellationToken)
    {
        var entities = await _chatRepository.GetByUserIdAsync(userId.Value, cancellationToken);

        return entities.Select(x => x.ToModel()).ToList();
    }

    public Task<ChatRoom> UpdateAsync(ChatRoom room, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
