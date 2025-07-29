using BoostEvents.Web.Application.Interfaces;
using BoostEvents.Web.Domain;
using BoostEvents.Web.Features.BusinessInstances.Requests;
using FastEndpoints;

namespace BoostEvents.Web.Features.BusinessInstances;

public class CreateBusinessInstanceEndpoint(IBusinessInstanceRepo _repo, ILogger<CreateBusinessInstanceEndpoint> _logger)
    : Endpoint<CreateBusinessInstanceRequest, BusinessInstance>
{
    public override void Configure()
    {
        Post("/business-instances");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateBusinessInstanceRequest req, CancellationToken ct)
    {
        var instance = new BusinessInstance
        {
            Name = req.Name,
            BusinessId = req.BusinessId,
            CreatedUtc = DateTime.UtcNow,
            IsDeleted = false
        };

        await _repo.CreateAsync(instance);

        _logger.LogInformation("Created business instance {@BusinessInstance}", instance);

        await SendCreatedAtAsync<CreateBusinessInstanceEndpoint>(
            new { id = instance.Id },
            instance,
            cancellation: ct);
    }
}
