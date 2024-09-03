using EventAPI.Features.Events.Create;
using EventAPI.Infrastructure.Database;
using EventAPI.Infrastructure.Minimal_API;
using EventAPI.model;
using MapsterMapper;
using MediatR;

namespace EventAPI.Features.Events
{
    public class REST : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/events/{id}", async (int id, ISender sender) =>
            {
                var result = await sender.Send(new GetEventById(id));
                return result != null ? Results.Ok(result) : Results.NotFound();
            });
        }
    }
    public class Handler : IRequestHandler<GetEventById, Event?>
    {
        private readonly EventDB db;
        
        public Handler(EventDB db)
        {
            this.db = db;
        }

        public async Task<Event?> Handle(GetEventById request, CancellationToken cancellationToken)
        {
            return await db.Events.FindAsync(request.EventId);
        }
    }
}
