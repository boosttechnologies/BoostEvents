using BoostEvents.Web.Application.Interfaces;
using BoostEvents.Web.Features.Businesses.Requests;
using FastEndpoints;

namespace BoostEvents.Web.Features.Businesses;

public class UpdateBusinessEndpoint(IBusinessRepo repo, ILogger<UpdateBusinessEndpoint> logger)
    : Endpoint<UpdateBusinessRequest>
{ 
    public override void Configure()
    {
        Put("/businesses/{id:guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateBusinessRequest req, CancellationToken ct)
    {
        var existing = await repo.ReadByIdAsync(req.Id);
        if (existing is null)
        {
            logger.LogWarning("Business not found for update: {Id}", req.Id);
            await SendNotFoundAsync(ct);
            return;
        }

        existing.Name = req.Name;
        existing.ModifiedUtc = DateTime.UtcNow;
        await repo.UpdateAsync(existing);
        logger.LogInformation("Business updated: {Id}", req.Id);

        await SendOkAsync(ct);
    }
}

