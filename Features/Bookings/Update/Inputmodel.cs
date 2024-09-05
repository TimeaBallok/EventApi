using EventAPI.model;
using MediatR;

namespace EventAPI.Features.Bookings.Update
{
    public record UpdateBooking(int BookingId, DateTime BookingDate, int UserId, int EventId) : IRequest<Booking?>;
}
