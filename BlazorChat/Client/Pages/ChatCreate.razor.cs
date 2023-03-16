using BlazorChat.Client.Extensions;
using BlazorChat.Client.Models.Feeds.Chat;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorChat.Client.Pages;

public partial class ChatCreate
{
    private EditContext? EditContext;
    private ValidationMessageStore? MessageStore;
    private readonly FieldIdentifier ErrorMessage;
    private readonly ChatCreateInput createInput = new();

    protected override async Task OnInitializedAsync()
    {
        EditContext = new(createInput);
        EditContext.OnValidationRequested += HandleValidationRequested;
        EditContext.OnFieldChanged += HandleFieldChanged;
        MessageStore = new(EditContext);

        await SetUserCheckboxes();
        await base.OnInitializedAsync();
    }

    private async Task SetUserCheckboxes()
    {
        var response = await UserRequests.GetAsync();

        createInput.UserCheckboxes = response.Users.ToFeed();
    }

    private void HandleValidationRequested(object? sender,
        ValidationRequestedEventArgs args)
    {
        MessageStore?.Clear();
    }

    private async Task HandleValidSubmit()
    {
        MessageStore?.Clear();
        await ChatHub.RequestCreateChatAsync(createInput.ToRequest());

        NavManager.NavigateTo("/");
    }

    private void HandleFieldChanged(object? sender,
        FieldChangedEventArgs args)
    {
        MessageStore?.Clear();
    }

    public void Dispose()
    {
        if (EditContext is not null)
        {
            EditContext.OnValidationRequested -= HandleValidationRequested;
            EditContext.OnFieldChanged -= HandleFieldChanged;
        }
    }
}
