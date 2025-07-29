using BoostEvents.Web.Application.Interfaces;
using BoostEvents.Web.Domain;
using FastEndpoints;

namespace BoostEvents.Web.Features.BusinessInstances;

public class GetBusinessInstancesEndpoint(IBusinessInstanceRepo _repo, IUserInfo userInfo, ILogger<GetBusinessInstancesEndpoint> _logger)
    : EndpointWithoutRequest<List<BusinessInstance>>
{
    public override void Configure()
    {
        Get("/business-instances");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var businessInstances = await _repo.ReadAsync(userInfo.TenantId);
        await SendAsync(businessInstances.ToList(), cancellation: ct);
    }
}