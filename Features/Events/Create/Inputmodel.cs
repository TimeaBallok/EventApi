using EventAPI.model;
using MediatR;

namespace EventAPI.Features.Events.Create
{
    public record CreateEvent(string Title, string Location, string Description, int Price) : IRequest<Event>;
}
