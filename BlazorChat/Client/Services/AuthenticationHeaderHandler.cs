﻿using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace BlazorChat.Client.Services;

public class AuthenticationHeaderHandler : DelegatingHandler
{
    private readonly ILocalStorageService localStorage;

    public AuthenticationHeaderHandler(ILocalStorageService localStorage)
        => this.localStorage = localStorage;

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (request.Headers.Authorization?.Scheme != "Bearer")
        {
            var savedToken = await localStorage.GetItemAsync<string>("authToken");

            if (!string.IsNullOrWhiteSpace(savedToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
