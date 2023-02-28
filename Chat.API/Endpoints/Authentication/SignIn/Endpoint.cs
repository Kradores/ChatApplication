﻿using Carter;
using Chat.Domain.Factories.Authentication;
using Chat.Domain.Models.Authentication.Aggregates;
using Chat.Domain.Models.Authentication.ValueObjects;

namespace Chat.API.Endpoints.Authentication.SignIn;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("sign-in", Handler)
            .WithName(nameof(SignIn))
            .WithTags(nameof(Authentication))
            .Produces(StatusCodes.Status200OK, typeof(void))
            .Produces(StatusCodes.Status401Unauthorized, typeof(void))
            .AllowAnonymous();
    }

    private static async Task<IResult> Handler(
        Request request,
        IAuthenticationFactory factory,
        CancellationToken cancellationToken)
    {
        var user = new User()
        {
            Username = Username.From(request.Username),
            Password = Password.From(request.Password)
        };

        var result = await factory.SignInAsync(user);

        return Results.Ok(result);
    }
}
