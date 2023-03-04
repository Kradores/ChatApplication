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
                .AddCarter()
                .AddRazorPages();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

            app.UseCookiePolicy();

            app.UseRouting();

            app.MapRazorPages();
            app.MapCarter();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}