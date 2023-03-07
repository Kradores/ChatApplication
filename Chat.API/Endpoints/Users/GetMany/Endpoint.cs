using Carter;
using Chat.Domain.Factories.Interfaces;
using static Chat.API.Endpoints.Users.GetMany.Response;

namespace Chat.API.Endpoints.Users.GetMany;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("user", Handler)
            .WithName(nameof(Users) + nameof(GetMany))
            .WithTags(nameof(Users))
            .Produces(StatusCodes.Status200OK, typeof(Response))
            .Produces(StatusCodes.Status401Unauthorized, typeof(void))
            .Produces(StatusCodes.Status409Conflict, typeof(void))
            .RequireAuthorization();
    }

    private static async Task<IResult> Handler(
        IUserFactory factory,
        CancellationToken cancellationToken)
    {
        var users = await factory.GetAsync(cancellationToken);

        return Results.Ok(new Response()
        {
            Users = users.Select(x => new User()
            {
                Id = x.Id.Value,
                Name = x.Username.Value
            }).ToList()
        });
    }
}
