using Carter;
using Chat.API.Configurations;

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
                .AddRepositories()
                .AddCarter();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.MapCarter();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}