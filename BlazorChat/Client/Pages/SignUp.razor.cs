﻿using BlazorChat.Client.Models.Requests.Authentication;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorChat.Client.Pages;

public partial class SignUp
{
    private readonly SignUpRequest Request = new();
    private EditContext? EditContext;
    private ValidationMessageStore? MessageStore;
    private readonly FieldIdentifier ErrorMessage;

    protected override void OnInitialized()
    {
        EditContext = new(Request);
        EditContext.OnValidationRequested += HandleValidationRequested;
        EditContext.OnFieldChanged += HandleFieldChanged;
        MessageStore = new(EditContext);
    }

    private void HandleValidationRequested(object? sender,
        ValidationRequestedEventArgs args)
    {
        MessageStore?.Clear();
    }

    private async Task HandleValidSubmit()
    {
        MessageStore?.Clear();
        var response = await AuthApi.RegisterUserAsync(Request);

        if (!response)
        {
            MessageStore?.Add(() => ErrorMessage, "Username already exists!");
        }
        else
        {
            NavManager.NavigateTo("/");
        }
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
