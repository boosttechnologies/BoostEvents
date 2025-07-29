using BoostEvents.Web.Application.Interfaces;
using BoostEvents.Web.Infrastructure.Db;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace BoostEvents.Web.Features.Businesses;

public class SoftDeleteBusinessRequest
{
    [FromRoute]
    public Guid Id { get; set; }
}

public class SoftDeleteBusinessEndpoint(IBusinessRepo repo, ILogger<SoftDeleteBusinessEndpoint> logger)
    : Endpoint<SoftDeleteBusinessRequest>
{
    public override void Configure()
    {
        Delete("/businesses/{id:guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(SoftDeleteBusinessRequest req, CancellationToken ct)
    {
        var existing = await repo.ReadByIdAsync(req.Id);
        if (existing is null)
        {
            logger.LogWarning("Business not found for deletion: {Id}", req.Id);
            await SendNotFoundAsync(ct);
            return;
        }
        
        await repo.SoftDeleteAsync(req.Id);
        logger.LogInformation("Business soft-deleted: {Id}", req.Id);

        await SendOkAsync(ct);
    }
}