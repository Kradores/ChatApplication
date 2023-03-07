using BlazorChat.Client.Configurations;
using BlazorChat.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorChat.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            builder.Services.AddHttpClient("Chat.API", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Chat.API"));


            builder.Services.AddOidcAuthentication(options =>
            {
                options.ProviderOptions.Authority = builder.HostEnvironment.BaseAddress;
                options.ProviderOptions.ClientId = "ClientId";
            });

            builder.Services.AddApiRequests()
                .AddStateContainers();

            await builder.Build().RunAsync();
        }
    }
}