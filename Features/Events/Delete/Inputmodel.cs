using EventAPI.model;
using MediatR;

namespace EventAPI.Features.Events.Delete
{
    public record DeleteEvent(int EventId) : IRequest<Event>;
}
