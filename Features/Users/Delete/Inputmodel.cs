using EventAPI.model;
using MediatR;

namespace EventAPI.Features.Users.Delete
{
    public record DeleteUser(int UserId) : IRequest<User>;
}
