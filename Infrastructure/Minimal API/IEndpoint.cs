namespace EventAPI.Infrastructure.Minimal_API
{
    public interface IEndpoint
    {
        void MapEndpoint(IEndpointRouteBuilder app);
    }
}
