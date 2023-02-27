using Chat.Domain.Factories.Authentication;
using Chat.Domain.Models.Authentication.Aggregates;
using Chat.Domain.Models.Authentication.ValueObjects;
using Chat.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Chat.API.Endpoints.Authentication.Register;
public static class Endpoint
{
    public static void AddRoutes(this IEndpointRouteBuilder app)
    {
        app.MapPost("register", Handler)
            .WithName(nameof(Register))
            .WithTags(nameof(Authentication))
            .Produces(StatusCodes.Status200OK, typeof(void))
            .Produces(StatusCodes.Status401Unauthorized, typeof(void))
            .AllowAnonymous();
    }

    private static async Task<IResult> Handler(Request request,
        IAuthenticationFactory factory,
        IUserRepository userRepository,
        CancellationToken cancellationToken)
    {
        var user = new User()
        {
            Username = Username.From(request.Username),
            Password = Password.From(request.Password)
        };

        var result = await factory.RegisterAsync(user, cancellationToken);

        if (result.Errors.Any())
        {
            return Results.BadRequest(result.Errors);
        }

        return Results.Ok(result);
    }
}
