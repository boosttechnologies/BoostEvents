using BoostEvents.Web.Application.Interfaces;
using BoostEvents.Web.Domain;
using BoostEvents.Web.Infrastructure.Db;
using FastEndpoints;

namespace BoostEvents.Web.Features.Businesses;

public class GetBusinessesEndpoint(IBusinessRepo _repo, ILogger<GetBusinessesEndpoint> _logger, IUserInfo user) : EndpointWithoutRequest<List<Business>>
{
    public override void Configure()
    {
        Get("/businesses");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var businesses = (await _repo.ReadAsync(user.TenantId)).ToList();
        _logger.LogInformation("Returning {Count} businesses", businesses.Count);
        await SendAsync(businesses, cancellation: ct);
    }
}