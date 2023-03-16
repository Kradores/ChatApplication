namespace BlazorChat.Client.Shared;

public partial class NavMenu
{
    private bool collapseNavMenu = true;
    private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

    private void ToggleNavMenu()
    {
        collapseNavMenu = !collapseNavMenu;
    }

    protected override void OnInitialized()
    {
        ChatHub.OnChatListStateChange += StateHasChanged;
    }

    public void Dispose()
    {
        ChatHub.OnChatListStateChange -= StateHasChanged;
    }
}
