using EventAPI.Features.Events.Create;
using EventAPI.Infrastructure.Database;
using EventAPI.Infrastructure.Minimal_API;
using EventAPI.model;
using MapsterMapper;
using MediatR;

namespace EventAPI.Features.Events.Update
{
    public class REST : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("/events/{id}", async (int id, UpdateEvent updatedEvent, ISender sender) =>
            {
                if (id != updatedEvent.EventId)
                {
                    return Results.BadRequest("Event ID mismatch.");
                }

                var result = await sender.Send(updatedEvent);
                return result != null ? Results.Ok(result) : Results.NotFound();
            });
        }
    }
   

    public class Handler : IRequestHandler<UpdateEvent, Event>
    {
        private readonly EventDB db;
        private readonly IMapper mapper;

        public Handler(EventDB db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<Event> Handle(UpdateEvent request, CancellationToken cancellationToken)
        {
            var eventToUpdate = await db.Events.FindAsync(new object[] { request.EventId }, cancellationToken);

            if (eventToUpdate == null)
            {
                return null;
            }
            eventToUpdate.Title = request.Title;
            eventToUpdate.Location = request.Location;
            eventToUpdate.Description = request.Description;
            eventToUpdate.Price = request.Price;

            await db.SaveChangesAsync();

            return eventToUpdate;
        }
    }
}
