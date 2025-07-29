using BoostEvents.Web.Application.Interfaces;
using BoostEvents.Web.Domain;
using BoostEvents.Web.Features.Businesses.Requests;
using BoostEvents.Web.Infrastructure.Db;
using FastEndpoints;

namespace BoostEvents.Web.Features.Businesses;

public class CreateBusinessEndpoint(IBusinessRepo repo,  ILogger<CreateBusinessEndpoint> logger) : Endpoint<CreateBusinessRequest, EmptyResponse>
{
    public override void Configure()
    {
        Post("/businesses");
        AllowAnonymous(); // Adjust for auth if needed
        Summary(s => s.Summary = "Create a new business");
    }

    public override async Task HandleAsync(CreateBusinessRequest req, CancellationToken ct)
    {
        await repo.CreateAsync(new Business { Name = req.Name });
        await SendAsync(new EmptyResponse(), cancellation: ct);
    }
}

