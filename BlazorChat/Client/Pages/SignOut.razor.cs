namespace BlazorChat.Client.Pages;

public partial class SignOut
{
    protected override async Task OnInitializedAsync()
    {
        await SignOutAsync();
    }

    private async Task SignOutAsync()
    {
        await AuthApi.LogoutAsync();
        NavManager.NavigateTo("/");
    }
}
