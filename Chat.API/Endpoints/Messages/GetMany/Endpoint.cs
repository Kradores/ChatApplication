using Carter;
using Chat.Domain.Factories.Interfaces;
using Chat.Domain.Models.ValueObjects;
using Chat.Infrastructure.Filters;
using static Chat.API.Endpoints.Messages.GetMany.Response;

namespace Chat.API.Endpoints.Messages.GetMany;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("message", Handler)
            .WithName(nameof(Messages) + nameof(GetMany))
            .WithTags(nameof(Messages))
            .Produces(StatusCodes.Status200OK, typeof(Response))
            .Produces(StatusCodes.Status401Unauthorized, typeof(void))
            .Produces(StatusCodes.Status409Conflict, typeof(void))
            .RequireAuthorization();
    }

    private static async Task<IResult> Handler(
        Request request,
        IMessageFactory factory,
        CancellationToken cancellationToken)
    {
        var pagination = new Pagination(request.PageNumber, request.PageSize);
        var messages = await factory.GetAsync(Id.From(request.ChatId), pagination, cancellationToken);

        return Results.Ok(new Response()
        {
            Messages = messages.Select(x => new Message()
            {
                Id = x.Id.Value,
                ChatRoomId = x.ChatId.Value,
                Text = x.Text.Value,
                CreatedAt = x.CreatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                SenderName = x.User.Username.Value
            }).ToList()
        });
    }
}
