using BoostEvents.Web.Application.Interfaces;
using BoostEvents.Web.Features.BusinessInstances.Requests;
using FastEndpoints;

namespace BoostEvents.Web.Features.BusinessInstances;

public class SoftDeleteBusinessInstanceEndpoint(IBusinessInstanceRepo _repo, ILogger<SoftDeleteBusinessInstanceEndpoint> _logger)
    : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("/business-instances/{id:guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        _logger.LogInformation("Fetching delete businessInstance by ID: {Id}", id);
        
        var existing = await _repo.ReadByIdAsync(id);
        if (existing is null)
        {
            _logger.LogWarning("Business instance not found: {Id}", id);
            await SendNotFoundAsync(ct);
            return;
        }

        await _repo.SoftDeleteAsync(id);
        _logger.LogInformation("Business instance soft deleted: {Id}", id);
        await SendOkAsync(ct);
    }
}