using EventAPI.model;
using MediatR;

namespace EventAPI.Features.Bookings.Get_All
{
    public record GetAllBookings() : IRequest<IEnumerable<Booking>>;
}
