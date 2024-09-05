using EventAPI.Infrastructure.Database;
using EventAPI.Infrastructure.Minimal_API;
using EventAPI.model;
using MapsterMapper;
using MediatR;

namespace EventAPI.Features.Bookings.Create
{
    [MutationType]
    public class BookingMutation
    {
        public async Task<Booking> CreateBookingMutation(CreateBooking booking, [Service] ISender sender)
        {
            var newBooking = await sender.Send(booking);
            return newBooking;
        }
    }
    public class REST : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/bookings", async (CreateBooking booking, ISender sender) =>
            {
                var newBooking = await sender.Send(booking);

                return Results.Created($"/bookings/{newBooking.BookingId}", newBooking);
            });
        }
    }
    public class Handler : IRequestHandler<CreateBooking, Booking>
    {
        private readonly EventDB db;
        private readonly IMapper mapper;

        public Handler(EventDB db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<Booking> Handle(CreateBooking request, CancellationToken cancellationToken)
        {
            var newBooking = mapper.Map<Booking>(request);
            db.Bookings.Add(newBooking);
            await db.SaveChangesAsync();
            return newBooking;
        }
    }
}
