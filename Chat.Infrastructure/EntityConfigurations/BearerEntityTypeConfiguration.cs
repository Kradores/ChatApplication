using Chat.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infrastructure.EntityConfigurations;

public class BearerEntityTypeConfiguration : IEntityTypeConfiguration<Bearer>
{
    public void Configure(EntityTypeBuilder<Bearer> builder)
    {
        builder.ToTable("BearerTokens", ChatContext.DEFAULT_SCHEMA);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}
