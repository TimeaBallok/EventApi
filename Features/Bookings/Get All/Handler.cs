using EventAPI.Infrastructure.Database;
using EventAPI.Infrastructure.Minimal_API;
using EventAPI.model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventAPI.Features.Bookings.Get_All
{
    [QueryType]
    public class BookingQuery
    {
        public async Task<IEnumerable<Booking>> GetBookings([Service] ISender sender)
        {
            var result = await sender.Send(new GetAllBookings());
            return result;
        }
    }
    public class REST : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/bookings", async (ISender sender) =>
            {
                var result = await sender.Send(new GetAllBookings());
                return Results.Ok(result);

            });
        }
    }

    public class Handler : IRequestHandler<GetAllBookings, IEnumerable<Booking>>
    {
        private readonly EventDB db;

        public Handler(EventDB db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Booking>> Handle(GetAllBookings request, CancellationToken cancellationToken)
        {
            return await db.Bookings.ToListAsync(cancellationToken);
        }
    }
}
