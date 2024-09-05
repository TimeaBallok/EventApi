using EventAPI.model;
using MediatR;

namespace EventAPI.Features.Users.Update
{
    public record UpdateUser(int UserId, string Name) : IRequest<User?>;
}
