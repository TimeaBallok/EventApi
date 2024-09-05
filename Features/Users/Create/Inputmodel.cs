using EventAPI.model;
using MediatR;

namespace EventAPI.Features.Users.Create
{
    public record CreateUser(string Name) : IRequest<User>;
}
