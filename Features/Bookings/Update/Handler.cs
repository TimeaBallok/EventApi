using EventAPI.Features.Events.Update;
using EventAPI.Infrastructure.Database;
using EventAPI.Infrastructure.Minimal_API;
using EventAPI.model;
using MapsterMapper;
using MediatR;

namespace EventAPI.Features.Bookings.Update
{
    public class REST : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("/bookings/{id}", async (int id, UpdateBooking updatedBooking, ISender sender) =>
            {
                if (id != updatedBooking.BookingId)
                {
                    return Results.BadRequest("Event ID mismatch.");
                }

                var result = await sender.Send(updatedBooking);
                return result != null ? Results.Ok(result) : Results.NotFound();
            });
        }
    }


    public class Handler : IRequestHandler<UpdateBooking, Booking>
    {
        private readonly EventDB db;
        private readonly IMapper mapper;

        public Handler(EventDB db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<Booking> Handle(UpdateBooking request, CancellationToken cancellationToken)
        {
            var bookingToUpdate = await db.Bookings.FindAsync(new object[] { request.EventId }, cancellationToken);
            if (bookingToUpdate == null)
            {
                return null;
            }
            bookingToUpdate.BookingDate = request.BookingDate;
            bookingToUpdate.EventId = request.EventId;
            bookingToUpdate.UserId = request.UserId;
            await db.SaveChangesAsync(cancellationToken);
            return bookingToUpdate;
        }
        

        //public async Task<Event> Handle(UpdateEvent request, CancellationToken cancellationToken)
        //{
        //    var eventToUpdate = await db.Events.FindAsync(new object[] { request.EventId }, cancellationToken);

        //    if (eventToUpdate == null)
        //    {
        //        return null;
        //    }
        //    eventToUpdate.Title = request.Title;
        //    eventToUpdate.Location = request.Location;
        //    eventToUpdate.Description = request.Description;
        //    eventToUpdate.Price = request.Price;

        //    await db.SaveChangesAsync();

        //    return eventToUpdate;
        //}
    }
}
