using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorChat.Client.Services;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    public CustomAuthenticationStateProvider()
    {
        this.CurrentUser = this.GetAnonymous();
    }

    private ClaimsPrincipal CurrentUser { get; set; }


    private ClaimsPrincipal GetUser(string username)
    {

        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, username)
        }, "AuthCookie");
        return new ClaimsPrincipal(identity);
    }


    private ClaimsPrincipal GetAnonymous()
    {
        var identity = new ClaimsIdentity(new[]
       {
            new Claim(ClaimTypes.Name, "Anonymous")
        }, null);

        return new ClaimsPrincipal(identity);
    }


    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var task = Task.FromResult(new AuthenticationState(this.CurrentUser));

        return task;
    }

    public Task<AuthenticationState> ChangeUser(string username)
    {
        this.CurrentUser = this.GetUser(username);
        var task = this.GetAuthenticationStateAsync();
        this.NotifyAuthenticationStateChanged(task);

        return task;
    }

    public Task<AuthenticationState> SignOut()
    {
        this.CurrentUser = this.GetAnonymous();
        var task = this.GetAuthenticationStateAsync();
        this.NotifyAuthenticationStateChanged(task);

        return task;
    }
}
