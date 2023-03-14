using Carter;
using Chat.Domain.Factories.Interfaces;
using Chat.Domain.Models.Authentication.Aggregates;
using Chat.Domain.Models.Authentication.ValueObjects;

namespace Chat.API.Endpoints.Authentication.SignUp;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("sign-up", Handler)
            .WithName(nameof(SignUp))
            .WithTags(nameof(Authentication))
            .Produces(StatusCodes.Status200OK, typeof(Response))
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

        var result = await factory.SignUpAsync(user, cancellationToken);

        if (result == null)
        {
            return Results.BadRequest();
        }

        return Results.Ok(new Response()
        {
            Token = result.Token.Value,
            RefreshToken = result.RefreshToken.Value,
            RefreshTokenExpiryTime = result.RefreshTokenExpiryTime.Value,
        });
    }
}
