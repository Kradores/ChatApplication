using System.ComponentModel.DataAnnotations;

namespace BlazorChat.Client.Models.Authentication;

public class SignInRequest
{
    [Required]
    public string Username { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}
