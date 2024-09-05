using EventAPI.model;
using MediatR;
using OneOf;

namespace EventAPI.Features.Events.Get_All
{
    public enum APITypes
    {
        GraphQL,
        REST
    }
    public record GetAllEvents(APITypes Type) : IRequest<OneOf<IEnumerable<Event>, IQueryable<Event>>>;
}
