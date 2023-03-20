using AutoFixture;
using Chat.Domain.Factories.Interfaces;
using Chat.Domain.Models.Authentication.ValueObjects;
using Chat.Domain.Models.ValueObjects;
using Chat.Tests.Integration.Seeds;
using Chat.Tests.Integration.Setup;
using Chat.Tests.Integration.TestBases;
using ChatApplication;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Tests.Integration;

public class ChatRepositoryTest : TestBase
{
    private readonly IChatFactory _chatFactory;
    private readonly Fixture _fixture;

    public ChatRepositoryTest(IntegrationTestFactory<Program> factory) : base(factory)
    {
        var scope = factory.Services.CreateScope();
        _chatFactory = scope.ServiceProvider.GetRequiredService<IChatFactory>();
        _fixture = new();
    }

    [Fact]
    public async Task CreateAsync_ReturnsCreatedEntity()
    {
        var name = _fixture.Create<Name>();
        var userIds = UserSeeds.Users.Select(x => UserId.From(x.Id));

        var savedEntity = await _chatFactory.CreateAsync(name, userIds, default);

        Assert.NotNull(savedEntity);
        Assert.Equal(name.Value, savedEntity.Name.Value);
        Assert.NotNull(savedEntity.Id);

        var fetchedEntity = await _chatFactory.GetAsync(savedEntity.Id, default);

        Assert.NotNull(fetchedEntity);
        Assert.Equal(savedEntity.Name.Value, fetchedEntity.Name.Value);
    }

    [Fact]
    public async Task GetAsync_ReturnsChatsList()
    {
        var userId = UserId.From(UserSeeds.Users.First().Id);

        var entities = await _chatFactory.GetAsync(userId, default);

        Assert.NotNull(entities);
        Assert.True(entities.Count > 0);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public async Task GetAsync_ReturnsChatByName(int index)
    {
        var name = Name.From(ChatSeeds.Chats[index].Name);

        var entity = await _chatFactory.GetAsync(name, default);

        Assert.NotNull(entity);
        Assert.NotNull(entity.Id);
        Assert.True(entity.Id.Value > 0);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public async Task GetAsync_ReturnsChatById(int index)
    {
        var id = Id.From(ChatSeeds.Chats[index].Id);

        var entity = await _chatFactory.GetAsync(id, default);

        Assert.NotNull(entity);
        Assert.NotNull(entity.Id);
        Assert.True(entity.Id.Value > 0);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public async Task ResetUnreadMessages_ChatNotificationUnreadMessagesShouldBeZero(int index)
    {
        var id = Id.From(ChatSeeds.Chats[index].Id);
        var userId = UserId.From(ChatSeeds.Chats[index].Users.First().Id);

        var entity = await _chatFactory.ResetUnreadMessagesAsync(id, userId, default);

        Assert.NotNull(entity);

        var notif = entity.Notifications.Where(x => x.UserId.Value == userId.Value).FirstOrDefault();

        Assert.NotNull(notif);
        Assert.True(notif.UnreadMessagesCount.Value == 0);
    }
}
