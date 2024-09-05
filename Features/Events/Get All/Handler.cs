using EventAPI.Infrastructure.Database;
using EventAPI.Infrastructure.Minimal_API;
using EventAPI.model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace EventAPI.Features.Events.Get_All
{
    [QueryType]
    public class EventQuery
    {
        [UseOffsetPaging(DefaultPageSize = 20, MaxPageSize = 100, IncludeTotalCount = true)]
        [UseFiltering]
        [UseSorting]
        public async Task<IQueryable<Event>> GetEvents([Service] ISender sender)
        {
            var result = await sender.Send(new GetAllEvents(APITypes.GraphQL));
            return result;
        }
    }

    public class REST : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/events", async (ISender sender) =>
            {
                var result = await sender.Send(new GetAllEvents(APITypes.REST));
                return Results.Ok(result);
            });
        }
    }

    public class Handler : IRequestHandler<GetAllEvents, OneOf<IEnumerable<Event>, IQueryable<Event>>>
    {
        private readonly EventDB db;

        public Handler(EventDB db)
        {
            this.db = db;
        }

        public async Task<OneOf<IEnumerable<Event>, IQueryable<Event>>> Handle(GetAllEvents request, CancellationToken cancellationToken)
        {
            if (request.Type == APITypes.REST)
            {
                return await db.Events.ToListAsync(cancellationToken);
            }

            var x = await Task.Run(db.Events.AsQueryable);
            return x;
        }
    }
}
