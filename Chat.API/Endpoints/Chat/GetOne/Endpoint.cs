using Carter;
using Chat.Domain.Factories.Interfaces;
using Chat.Domain.Models.ValueObjects;

namespace Chat.API.Endpoints.Chat.GetOne;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("chat/{name}", Handler)
            .WithName(nameof(GetOne))
            .WithTags(nameof(Chat))
            .Produces(StatusCodes.Status200OK, typeof(Response))
            .Produces(StatusCodes.Status404NotFound, typeof(void))
            .Produces(StatusCodes.Status401Unauthorized, typeof(void))
            .RequireAuthorization();
    }

    private static async Task<IResult> Handler(
        string name,
        IChatFactory factory,
        CancellationToken cancellationToken)
    {
        var room = await factory.GetAsync(Name.From(name), cancellationToken);

        if (room == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(new Response()
        {
            Id = room.Id.Value,
            Name = room.Name.Value
        });
    }
}
