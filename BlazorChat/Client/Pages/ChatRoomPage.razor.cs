using BlazorChat.Client.Extensions;
using BlazorChat.Client.Models.Feeds.Messages;
using Microsoft.AspNetCore.Components;

namespace BlazorChat.Client.Pages;

public partial class ChatRoomPage
{
    [Parameter]
    public string? Id { get; set; } = null;

    private MessageInput Message { get; set; } = new();

    protected override void OnInitialized()
    {
        ChatHub.OnStateChange += StateHasChanged;
        ChatHub.OnMessageListStateChange += StateHasChanged;
    }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        if (!string.IsNullOrEmpty(Id))
        {
            ChatHub.ClearMessages();
            await ChatHub.RemoveFromGroupAsync(Id);
        }
        await base.SetParametersAsync(parameters);
    }

    protected override async Task OnParametersSetAsync()
    {
        if (!string.IsNullOrEmpty(Id))
        {
            await ChatHub.AddToGroupAsync(Id);

            var response = await api.GetAsync(new()
            {
                ChatId = int.Parse(Id),
                PageNumber = 1,
                PageSize = 50
            });

            ChatHub.InitMessages(response.ToFeed());
        }
    }

    private async Task Send()
    {
        if (!string.IsNullOrEmpty(Id))
        {
            await ChatHub.SendGroupMessageAsync(Id, Message.Text);
            Message.Text = string.Empty;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (!string.IsNullOrEmpty(Id)) await ChatHub.RemoveFromGroupAsync(Id);
        ChatHub.OnStateChange -= StateHasChanged;
        ChatHub.OnMessageListStateChange -= StateHasChanged;
    }
}
