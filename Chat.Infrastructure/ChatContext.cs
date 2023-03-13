using Chat.Infrastructure.Entities;
using Chat.Infrastructure.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure;

public class ChatContext : DbContext
{
    public const string CHAT_SCHEMA = "chat";
    public const string DEFAULT_SCHEMA = "generic";
    public const string MESSAGE_SCHEMA = "message";

    public ChatContext(DbContextOptions<ChatContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<ChatRoom> ChatRooms => Set<ChatRoom>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<MessageProperty> MessageProperties => Set<MessageProperty>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserReferenceEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ChatEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new MessageEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new MessagePropertyEntityTypeConfiguration());
    }
}
