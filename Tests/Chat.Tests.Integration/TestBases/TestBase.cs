using Chat.Infrastructure;
using Chat.Tests.Integration.Seeds;
using Chat.Tests.Integration.Setup;
using ChatApplication;
using Microsoft.EntityFrameworkCore;

namespace Chat.Tests.Integration.TestBases;

public class TestBase : IntegrationTestBase, IAsyncLifetime
{
    public TestBase(IntegrationTestFactory<Program> factory) : base(factory)
    {
        
    }

    private async Task AddUsers()
    {
        await ChatContext.Users.AddRangeAsync(UserSeeds.Users);
    }

    private async Task AddChats()
    {
        await ChatContext.ChatRooms.AddRangeAsync(ChatSeeds.Chats);
    }

    public async Task InitializeAsync()
    {
        await AuthenticationContext.Database.MigrateAsync();
        await ChatContext.Database.MigrateAsync();

        await AddUsers();
        await AddChats();

        await ChatContext.SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        await AuthenticationContext.Database.EnsureDeletedAsync();
        await ChatContext.Database.EnsureDeletedAsync();

        ChatSeeds.Chats.ForEach(x =>
        {
            x.Id = 0;
            x.Notifications.ToList().ForEach(x => x.Id = 0);
        });
    }
}
