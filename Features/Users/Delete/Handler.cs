using EventAPI.Features.Events.Delete;
using EventAPI.Infrastructure.Database;
using EventAPI.Infrastructure.Minimal_API;
using EventAPI.model;
using MediatR;

namespace EventAPI.Features.Users.Delete
{
    public class REST : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("/users/{id}", async (int id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteUser(id));
                return result != null ? Results.Ok($"User with id:{result.UserId} was removed") : Results.NotFound();
            });
        }
    }
    public class Handler : IRequestHandler<DeleteUser, User>
    {
        private readonly EventDB db;

        public Handler(EventDB db)
        {
            this.db = db;
        }

        public async Task<User> Handle(DeleteUser request, CancellationToken cancellationToken)
        {
            var userToDelete = await db.Users.FindAsync(request.UserId);
            if (userToDelete == null)
            {
                return null;
            }
            db.Users.Remove(userToDelete);
            await db.SaveChangesAsync();
            return userToDelete;
        }
    }
}
