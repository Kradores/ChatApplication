using Chat.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infrastructure.EntityConfigurations;

public class ChatEntityTypeConfiguration : IEntityTypeConfiguration<ChatRoom>
{
    public void Configure(EntityTypeBuilder<ChatRoom> builder)
    {
        builder.ToTable("ChatRooms", ChatContext.CHAT_SCHEMA);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasIndex(x => x.Name).IsUnique();

        builder.HasMany(x => x.Users)
            .WithMany()
            .UsingEntity<UserReference>();

        builder.HasMany(x => x.Notifications)
            .WithOne(x => x.ChatRoom)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Messages)
            .WithOne(x => x.ChatRoom)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
