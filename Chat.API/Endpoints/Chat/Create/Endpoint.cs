using Carter;
using Chat.Domain.Factories.Interfaces;
using Chat.Domain.Models.Authentication.ValueObjects;
using Chat.Domain.Models.ValueObjects;

namespace Chat.API.Endpoints.Chat.Create;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("chat", Handler)
            .WithName(nameof(Create))
            .WithTags(nameof(Chat))
            .Produces(StatusCodes.Status200OK, typeof(Response))
            .Produces(StatusCodes.Status401Unauthorized, typeof(void))
            .RequireAuthorization();
    }

    private static async Task<IResult> Handler(
        Request request,
        IChatFactory factory,
        CancellationToken cancellationToken)
    {
        var room = await factory.CreateAsync(Name.From(request.Name), request.UserIds.Select(UserId.From), cancellationToken);

        return Results.Ok(new Response()
        {
            Id = room.Id.Value,
            Name = room.Name.Value
        });
    }
}
