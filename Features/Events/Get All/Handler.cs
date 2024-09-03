using EventAPI.Infrastructure.Database;
using EventAPI.Infrastructure.Minimal_API;
using EventAPI.model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventAPI.Features.Events.Get_All
{
    public class REST : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/events", async (ISender sender) =>
            {
                var result = await sender.Send(new GetAllEvents());
                return Results.Ok(result);
                
            });
        }
    }

    public class Handler : IRequestHandler<GetAllEvents, IEnumerable<Event>>
    {
        private readonly EventDB db;

        public Handler(EventDB db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Event>> Handle(GetAllEvents request, CancellationToken cancellationToken)
        {
            return await db.Events.ToListAsync();
        }
    }
}
