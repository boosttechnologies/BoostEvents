using BoostEvents.Web.Application.Interfaces;
using BoostEvents.Web.Domain;
using BoostEvents.Web.Features.BusinessInstances.Requests;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace BoostEvents.Web.Features.BusinessInstances;

public class GetBusinessInstanceByIdEndpoint(IBusinessInstanceRepo _repo, ILogger<GetBusinessInstanceByIdEndpoint> _logger)
    : EndpointWithoutRequest<BusinessInstance>
{
    public override void Configure()
    {
        Get("/businesses-instances/{id:guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        _logger.LogInformation("Fetching businessInstance by ID: {Id}", id);

        var result = await _repo.ReadByIdAsync(id);
        if (result is null)
            await SendNotFoundAsync(ct);
        else
            await SendAsync(result, cancellation: ct);
    }
}
