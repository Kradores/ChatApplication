﻿namespace BlazorChat.Client.Models.Responses.Authentication;

public class TokenResponse
{
    public string Token { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public DateTime RefreshTokenExpiryTime { get; set; }
}
