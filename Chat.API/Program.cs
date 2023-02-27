using Chat.API.Configurations;
using Chat.API.Endpoints.Authentication.Register;
using System.Security.Claims;

namespace ChatApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.AddOptions();

            builder.Services
                .AddDbContext(builder.Configuration)
                .AddIdentityAuthentication()
                .AddWriters()
                .AddFactories()
                .AddRepositories();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting()
                .UseAuthentication()
                .UseEndpoints(x => x.AddRoutes());

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}