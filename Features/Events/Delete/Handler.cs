using EventAPI.Features.Events.Update;
using EventAPI.Infrastructure.Database;
using EventAPI.Infrastructure.Minimal_API;
using EventAPI.model;
using MediatR;

namespace EventAPI.Features.Events.Delete
{
    [MutationType]
    public class EventMutation
    {
        public async Task<Event?> DeleteEventMutation(int id, [Service] ISender sender)
        {
            var result = await sender.Send(new DeleteEvent(id));
            return result;
        }
    }
    public class REST : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("/events/{id}", async (int id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteEvent(id));
                return result != null ? Results.Ok($"Event with id:{result.EventId} was removed") : Results.NotFound();
            });
        }
    }
    public class Handler : IRequestHandler<DeleteEvent, Event>
    {
        private readonly EventDB db;

        public Handler(EventDB db)
        {
            this.db = db;
        }
        public async Task<Event> Handle(DeleteEvent request, CancellationToken cancellationToken)
        {
            var eventToDelete = await db.Events.FindAsync(request.EventId);
            if (eventToDelete != null)
            {
                db.Events.Remove(eventToDelete);
                await db.SaveChangesAsync();
                return eventToDelete;
            }

            return null;
                
        }
    }
}
