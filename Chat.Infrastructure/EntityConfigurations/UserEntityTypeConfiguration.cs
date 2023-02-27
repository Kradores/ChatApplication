using Chat.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infrastructure.EntityConfigurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", ChatContext.DEFAULT_SCHEMA);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        //builder.HasIndex(x => x.Username).IsUnique();

        builder.HasMany(x => x.ChatRooms)
            .WithMany(x => x.Users)
            .UsingEntity<UserReference>();

        //builder.HasOne(x => x.Bearer)
        //    .WithOne();
    }
}
