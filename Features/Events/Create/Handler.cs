using EventAPI.Infrastructure.Database;
using EventAPI.Infrastructure.Minimal_API;
using EventAPI.model;
using MapsterMapper;
using MediatR;

namespace EventAPI.Features.Events.Create
{
    public class REST : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/events", async (CreateEvent evnt, ISender sender) =>
            {
                var newEvent = await sender.Send(evnt);

                return Results.Created($"/events/{newEvent.EventId}", newEvent);
            });
        }
    }
    public class Handler : IRequestHandler<CreateEvent, Event>
    {
        private readonly EventDB db;
        private readonly IMapper mapper;

        public Handler(EventDB db, IMapper mapper) 
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<Event> Handle(CreateEvent request, CancellationToken cancellationToken)
        {
            var newEvent = mapper.Map<Event>(request);
            db.Events.Add(newEvent);
            await db.SaveChangesAsync();

            return newEvent;
        }
    }
}
