using EventAPI.Infrastructure.Database;
using EventAPI.Infrastructure.Minimal_API;
using EventAPI.model;
using MapsterMapper;
using MediatR;

namespace EventAPI.Features.Users.Update
{
    public class REST : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("/users/{id}", async (int id, UpdateUser updatedUser, ISender sender) =>
            {
                if (id != updatedUser.UserId)
                {
                    return Results.BadRequest("Event ID mismatch.");
                }

                var result = await sender.Send(updatedUser);
                return result != null ? Results.Ok(result) : Results.NotFound();
            });
        }
    }


    public class Handler : IRequestHandler<UpdateUser, User>
    {
        private readonly EventDB db;
        private readonly IMapper mapper;

        public Handler(EventDB db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<User> Handle(UpdateUser request, CancellationToken cancellationToken)
        {
            var userToUpdate = await db.Users.FindAsync(new object[] { request.UserId }, cancellationToken);
            if (userToUpdate == null)
            {
                return null;
            }
            userToUpdate.Name = request.Name;
            await db.SaveChangesAsync(cancellationToken);
            return userToUpdate;
        }
    }
}
