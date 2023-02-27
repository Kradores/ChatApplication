using Chat.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infrastructure.EntityConfigurations;

public class UserReferenceEntityTypeConfiguration : IEntityTypeConfiguration<UserReference>
{
    public void Configure(EntityTypeBuilder<UserReference> builder)
    {
        builder.ToTable("UserReferences", ChatContext.CHAT_SCHEMA);

        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.ChatRoomId).IsRequired();
    }
}
