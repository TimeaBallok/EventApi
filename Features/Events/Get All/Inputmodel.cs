using EventAPI.model;
using MediatR;

namespace EventAPI.Features.Events.Get_All
{
    public record GetAllEvents() : IRequest<IEnumerable<Event>>;
}
