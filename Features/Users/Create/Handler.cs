using EventAPI.Infrastructure.Database;
using EventAPI.Infrastructure.Minimal_API;
using EventAPI.model;
using MapsterMapper;
using MediatR;

namespace EventAPI.Features.Users.Create
{
    [MutationType]
    public class UserMutation
    {
        public async Task<User> CreateUserMutation(CreateUser user, [Service] ISender sender)
        {
            var newUser = await sender.Send(user);
            return newUser;
        }
    }
    public class REST : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/users", async (CreateUser user, ISender sender) =>
            {
                var newUser = await sender.Send(user);

                return Results.Created($"/users/{newUser.UserId}", newUser);
            });
        }
    }
    public class Handler : IRequestHandler<CreateUser, User>
    {
        private readonly EventDB db;
        private readonly IMapper mapper;

        public Handler(EventDB db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<User> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            var newUser = mapper.Map<User>(request);
            db.Users.Add(newUser);
            await db.SaveChangesAsync();

            return newUser;
        }
    }
}

