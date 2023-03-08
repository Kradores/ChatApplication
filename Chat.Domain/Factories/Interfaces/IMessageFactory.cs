using Chat.Domain.Models.Authentication.ValueObjects;
using Chat.Domain.Models.Messages.Aggregates;
using Chat.Domain.Models.Messages.VaulueObjects;
using Chat.Domain.Models.ValueObjects;
using Chat.Infrastructure.Filters;

namespace Chat.Domain.Factories.Interfaces;

public interface IMessageFactory
{
    Task<Message> CreateAsync(UserId senderId, Id chatId, Text text, CancellationToken cancellationToken);
    Task<Message> UpdateAsync(UserId userId, Id messageId, MessageStatus status, CancellationToken cancellationToken);
    Task<List<Message>> GetAsync(Id chatId, Pagination pagination, CancellationToken cancellationToken);
}
