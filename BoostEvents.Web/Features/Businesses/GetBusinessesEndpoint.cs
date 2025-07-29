using BoostEvents.Web.Application.Interfaces;
using BoostEvents.Web.Domain;
using BoostEvents.Web.Infrastructure.Db;
using FastEndpoints;

namespace BoostEvents.Web.Features.Businesses;

public class GetBusinessesEndpoint(IBusinessRepo repo, ILogger<GetBusinessesEndpoint> logger, ITenantInfo _tenant) : EndpointWithoutRequest<List<Business>>
{
    public override void Configure()
    {
        Get("/businesses");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var businesses = (await repo.ReadAsync(_tenant.TenantId)).ToList();
        logger.LogInformation("Returning {Count} businesses", businesses.Count);
        await SendAsync(businesses, cancellation: ct);
    }
}