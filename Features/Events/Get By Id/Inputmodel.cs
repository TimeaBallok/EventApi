using EventAPI.model;
using MediatR;

namespace EventAPI.Features.Events
{
    public record GetEventById(int EventId) : IRequest<Event?>;
}
