using System.ComponentModel.DataAnnotations;

namespace BlazorChat.Client.Models.Requests.Authentication;

public class SignUpRequest
{
    [Required]
    public string Username { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}
