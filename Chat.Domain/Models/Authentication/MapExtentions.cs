using Chat.Domain.Models.Authentication.Aggregates;
using Chat.Domain.Models.Authentication.ValueObjects;
using UserEntity = Chat.Infrastructure.Entities.User;

namespace Chat.Domain.Models.Authentication;

public static class MapExtentions
{
    public static User ToModel(this UserEntity entity)
    {
        if (entity.UserName is null)
        {
            throw new ArgumentNullException(nameof(entity.UserName));
        }

        return new User()
        {
            Id = UserId.From(entity.Id),
            Username = Username.From(entity.UserName)
        };
    }
}
