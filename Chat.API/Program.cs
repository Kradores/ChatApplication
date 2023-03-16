using Carter;
using Chat.API.Configurations;
using Chat.API.Hubs;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;

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
                .AddJwtAuthentication(builder.Services.GetApplicationSettings(builder.Configuration))
                .AddWriters()
                .AddFactories()
                .AddRepositories()
                .AddEvents()
                .AddBackgroundServices()
                .AddCarter()
                .AddSignalR();

            builder.Services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            var app = builder.Build();

            app.UseResponseCompression();

            if (app.Environment.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapCarter();
            app.MapHub<ChatHub>("/chathub");
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}