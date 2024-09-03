using EventAPI.model;
using MediatR;

namespace EventAPI.Features.Events.Update
{
    public record UpdateEvent(int EventId, string Title, string Location, string Description, int Price) : IRequest<Event?>;
}
