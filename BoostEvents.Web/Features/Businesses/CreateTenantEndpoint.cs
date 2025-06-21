using BoostEvents.Web.Infrastructure.Db;
using BoostEvents.Web.Models;
using FastEndpoints;

namespace BoostEvents.Web.Features.Businesses;

public class CreateTenantEndpoint(ITenantRepository repo, ILogger<CreateBusinessEndpoint> logger)
    : Endpoint<CreateTenantRequest, EmptyResponse>
{

    public override void Configure()
    {
        Post("tenant");
        AllowAnonymous();
        Summary(s => s.Summary = "Create a new tenant");
    }

    public override async Task HandleAsync(CreateTenantRequest req, CancellationToken ct)
    {
        var newTenant = new Tenant()
        {
            Name = req.Name,
        };
        await repo.InsertAsync(newTenant);
        logger.LogInformation("Tenant created: {Name}", newTenant.Name);
        await SendOkAsync(ct);
    }
}