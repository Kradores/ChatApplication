using Chat.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infrastructure.EntityConfigurations;

public class MessagePropertyEntityTypeConfiguration : IEntityTypeConfiguration<MessageProperty>
{
    public void Configure(EntityTypeBuilder<MessageProperty> builder)
    {
        builder.ToTable("MessageProperties", ChatContext.MESSAGE_SCHEMA);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.SenderId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.ReceiverId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<ChatRoom>()
            .WithMany()
            .HasForeignKey(x => x.ChatRoomId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<Message>()
            .WithMany(x => x.Properties)
            .HasForeignKey(x => x.MessageId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
