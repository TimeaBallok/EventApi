using EventAPI.model;
using MediatR;

namespace EventAPI.Features.Bookings.Create
{
    public record CreateBooking(DateTime BookingDate, int UserId, int EventId) : IRequest<Booking>;
}
