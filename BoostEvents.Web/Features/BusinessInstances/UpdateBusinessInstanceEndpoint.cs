using BoostEvents.Web.Application.Interfaces;
using BoostEvents.Web.Features.BusinessInstances.Requests;
using FastEndpoints;

namespace BoostEvents.Web.Features.BusinessInstances;

public class UpdateBusinessInstanceEndpoint(IBusinessInstanceRepo _repo, ILogger<UpdateBusinessInstanceEndpoint> _logger, IUserInfo userInfo)
    : Endpoint<UpdateBusinessInstanceRequest>
{
    public override void Configure()
    {
        Put("/business-instances/{id:guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateBusinessInstanceRequest req, CancellationToken ct)
    {
        var existing = await _repo.ReadByIdAsync(req.Id);
        if (existing is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        /*existing.TenantId = _tenantInfo.TenantId;*/
        existing.Name = req.Name;
        await _repo.UpdateAsync(existing);

        _logger.LogInformation("Business instance updated: {@Instance}", existing);
        await SendOkAsync(ct);
    }
}