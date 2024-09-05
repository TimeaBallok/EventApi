using EventAPI.model;
using MediatR;

namespace EventAPI.Features.Users
{
    public record GetUserById(int UserId) : IRequest<User?>;
}
