﻿using Chat.Infrastructure.Entities;
using Chat.Infrastructure.Filters;

namespace Chat.Infrastructure.Repositories.Interfaces;

public interface IMessageRepository
{
    Task CreateAsync(Message message);
    Task UpdateAsync(Message message);
    Task<List<Message>> GetAsync(int chatId, Pagination pagination, CancellationToken cancellationToken);
    Task<Message?> GetAsync(int id, CancellationToken cancellationToken);
}