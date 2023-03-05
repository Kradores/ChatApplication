using Carter;
using Chat.Domain.Factories.Interfaces;

namespace Chat.API.Endpoints.Authentication.SignOut;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("sign-out", Handler)
            .WithName(nameof(SignOut))
            .WithTags(nameof(Authentication))
            .Produces(StatusCodes.Status200OK, typeof(void))
            .Produces(StatusCodes.Status401Unauthorized, typeof(void))
            .RequireAuthorization();
    }

    private static async Task<IResult> Handler(
        IAuthenticationFactory factory,
        CancellationToken cancellationToken)
    {
        await factory.SignOutAsync();

        return Results.Ok();
    }
}
