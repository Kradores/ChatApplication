using Chat.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Tests.Integration.Setup;

public class IntegrationTestFactory<TProgram> : WebApplicationFactory<TProgram>, IAsyncLifetime
    where TProgram : class
{
    private readonly MsSqlContainer _msSqlContainer;

    public Task InitializeAsync()
    {
        return _msSqlContainer.StartAsync();
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        return _msSqlContainer.DisposeAsync().AsTask();
    }

    public IntegrationTestFactory()
    {
        _msSqlContainer = new MsSqlBuilder().Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            AttachDbContext<AuthenticationContext>(services);
            AttachDbContext<ChatContext>(services);
        });
    }

    private void AttachDbContext<TDbContext>(IServiceCollection services) where TDbContext : DbContext
    {
        services.RemoveDbContext<TDbContext>();
        services.AddDbContext<TDbContext>(options =>
        {
            options.UseSqlServer(_msSqlContainer.GetConnectionString());
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });
        services.AddTransient<TDbContext>();
    }
}
