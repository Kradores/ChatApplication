using System.ComponentModel.DataAnnotations;

namespace BlazorChat.Client.Models.Feeds.Messages;

public class MessageInput
{
    [Required]
    [MinLength(1)]
    [MaxLength(255)]
    public string Text { get; set; } = string.Empty;
}
