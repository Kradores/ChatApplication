using Chat.Infrastructure;
using ChatApplication;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Tests.Integration.Setup;

public class IntegrationTestBase : IClassFixture<IntegrationTestFactory<Program>>
{
    public readonly IntegrationTestFactory<Program> Factory;
    public readonly AuthenticationContext AuthenticationContext;
    public readonly ChatContext ChatContext;

    public IntegrationTestBase(IntegrationTestFactory<Program> factory)
    {
        Factory = factory;
        var scope = factory.Services.CreateScope();
        AuthenticationContext = scope.ServiceProvider.GetRequiredService<AuthenticationContext>();
        ChatContext = scope.ServiceProvider.GetRequiredService<ChatContext>();
    }
}
