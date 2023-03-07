using Carter;
using Chat.Domain.Factories.Interfaces;
using Chat.Domain.Models.Authentication.ValueObjects;
using System.Security.Claims;
using static Chat.API.Endpoints.Chat.GetMany.Response;

namespace Chat.API.Endpoints.Chat.GetMany;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("chat", Handler)
            .WithName(nameof(Chat) + nameof(GetMany))
            .WithTags(nameof(Chat))
            .Produces(StatusCodes.Status200OK, typeof(Response))
            .Produces(StatusCodes.Status401Unauthorized, typeof(void))
            .Produces(StatusCodes.Status409Conflict, typeof(void))
            .RequireAuthorization();
    }

    private static async Task<IResult> Handler(
        HttpContext context,
        IChatFactory factory,
        CancellationToken cancellationToken)
    {
        var userId = context.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;

        if (userId == null)
        {
            return Results.Conflict("NameIdentifier is missing, contact support");
        }

        var rooms = await factory.GetAsync(UserId.From(userId), cancellationToken);

        return Results.Ok(new Response()
        {
            Rooms = rooms.Select(x => new Room()
            {
                Id = x.Id.Value,
                Name = x.Name.Value
            }).ToList()
        });
    }
}
