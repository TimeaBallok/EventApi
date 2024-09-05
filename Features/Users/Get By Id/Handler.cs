using EventAPI.Infrastructure.Database;
using EventAPI.Infrastructure.Minimal_API;
using EventAPI.model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace EventAPI.Features.Users
{
    public class REST : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/users/{id}", async (int id, ISender sender) =>
            {
                var result = await sender.Send(new GetUserById(id));

                return result != null ? Results.Ok(result) : Results.NotFound();
            });
        }
    }
    public class Handler : IRequestHandler<GetUserById, User?>
    {
        private readonly EventDB db;
        
        public Handler(EventDB db)
        {
            this.db = db;
        }

        public async Task<User?> Handle(GetUserById request, CancellationToken cancellationToken)
        {
            return await db.Users.Include(u => u.Bookings).FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);

            //return await db.Users
            //            .Include(u => u.Bookings) 
            //            .ThenInclude(b => b.Event) 
            //            .FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);
        }
    }
}
