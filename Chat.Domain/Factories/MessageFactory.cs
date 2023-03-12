using Chat.Domain.Factories.Interfaces;
using Chat.Domain.Models.Authentication.ValueObjects;
using Chat.Domain.Models.Messages;
using Chat.Domain.Models.Messages.Aggregates;
using Chat.Domain.Models.Messages.VaulueObjects;
using Chat.Domain.Models.ValueObjects;
using Chat.Infrastructure.Filters;
using Chat.Infrastructure.Repositories.Interfaces;
using MessageEntity = Chat.Infrastructure.Entities.Message;
using MessagePropertyEntity = Chat.Infrastructure.Entities.MessageProperty;

namespace Chat.Domain.Factories;

public class MessageFactory : IMessageFactory
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;
    private readonly IChatRepository _chatRepository;

    public MessageFactory(IMessageRepository messageRepository, IUserRepository userRepository, IChatRepository chatRepository)
    {
        _messageRepository = messageRepository;
        _userRepository = userRepository;
        _chatRepository = chatRepository;
    }

    public async Task<Message> CreateAsync(UserId senderId, Id chatId, Text text, CancellationToken cancellationToken)
    {
        var senderEntityTask = _userRepository.GetAsync(senderId.Value, cancellationToken);
        var chatEntityTask = _chatRepository.GetAsync(chatId.Value, cancellationToken);

        var senderEntity = await senderEntityTask;
        var chatEntity = await chatEntityTask;

        if (senderEntity == null)
        {
            throw new ArgumentNullException(nameof(senderEntity));
        }

        if (chatEntity == null)
        {
            throw new ArgumentNullException(nameof(chatEntity));
        }

        await _chatRepository.AttachUsersAsync(chatEntity);

        var properties = chatEntity.Users.Select(x => new MessagePropertyEntity()
        {
            SenderId = senderId.Value,
            ReceiverId = x.Id,
            ChatRoomId = chatId.Value,
        }).ToList();

        var messageEntity = new MessageEntity()
        {
            UserId = senderId.Value,
            ChatRoomId = chatId.Value,
            Data = text.Value,
            Properties = properties
        };

        await _messageRepository.CreateAsync(messageEntity);
        await _messageRepository.AttachUserAsync(messageEntity);

        return messageEntity.ToModel();
    }

    public async Task<Message> CreateAsync(UserId senderId, Name chatName, Text text)
    {
        var senderEntityTask = _userRepository.GetAsync(senderId.Value, default);
        var chatEntityTask = _chatRepository.GetAsync(chatName.Value, default);

        var senderEntity = await senderEntityTask;
        var chatEntity = await chatEntityTask;

        if (senderEntity == null)
        {
            throw new ArgumentNullException(nameof(senderEntity));
        }

        if (chatEntity == null)
        {
            throw new ArgumentNullException(nameof(chatEntity));
        }

        await _chatRepository.AttachUsersAsync(chatEntity);

        var properties = chatEntity.Users.Select(x => new MessagePropertyEntity()
        {
            SenderId = senderId.Value,
            ReceiverId = x.Id,
            ChatRoomId = chatEntity.Id,
        }).ToList();

        var messageEntity = new MessageEntity()
        {
            UserId = senderId.Value,
            ChatRoomId = chatEntity.Id,
            Data = text.Value,
            Properties = properties
        };

        await _messageRepository.CreateAsync(messageEntity);

        return messageEntity.ToModel();
    }

    public async Task<List<Message>> GetAsync(Id chatId, Pagination pagination, CancellationToken cancellationToken)
    {
        var chatEntity = await _chatRepository.GetAsync(chatId.Value, cancellationToken);

        if (chatEntity == null)
        {
            throw new ArgumentNullException(nameof(chatEntity));
        }

        var messages = await _messageRepository.GetAsync(chatId.Value, pagination, cancellationToken);

        foreach (var message in messages)
        {
            await _messageRepository.AttachUserAsync(message);
        }

        return messages.ToModel();
    }

    public async Task<Message> UpdateAsync(UserId userId, Id messageId, MessageStatus status, CancellationToken cancellationToken)
    {
        var userEntityTask = _userRepository.GetAsync(userId.Value, cancellationToken);
        var messageEntityTask = _messageRepository.GetAsync(messageId.Value, cancellationToken);

        var userEntity = await userEntityTask;
        var messageEntity = await messageEntityTask;

        if (userEntity == null)
        {
            throw new ArgumentNullException(nameof(userEntity));
        }

        if (messageEntity == null)
        {
            throw new ArgumentNullException(nameof(messageEntity));
        }

        await _messageRepository.AttachPropertiesAsync(messageEntity);

        var message = messageEntity.ToModel();

        message.UpdatePropertyStatus(userId, status);
        UpdateTrackedEntity(messageEntity, message);

        await _messageRepository.UpdateAsync(messageEntity);

        return message;
    }

    private static void UpdateTrackedEntity(MessageEntity entity, Message model)
    {
        foreach (var propertyEntity in entity.Properties)
        {
            var propertyModel = model.Properties.Where(x => x.Id.Value ==  propertyEntity.Id).FirstOrDefault();

            if (propertyModel != null)
            {
                propertyEntity.Status = propertyModel.Status.Value;
            }
        }
    }
}
