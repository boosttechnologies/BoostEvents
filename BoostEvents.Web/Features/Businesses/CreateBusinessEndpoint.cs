using BoostEvents.Web.Infrastructure.Db;
using BoostEvents.Web.Models;
using FastEndpoints;

namespace BoostEvents.Web.Features.Businesses;

public class CreateBusinessEndpoint(IBusinessRepository repo, ILogger<CreateBusinessEndpoint> logger)
    : Endpoint<CreateBusinessRequest, EmptyResponse>
{

    public override void Configure()
    {
        Post("business");
        AllowAnonymous();
        Summary(s => s.Summary = "Create a new business");
    }

    public override async Task HandleAsync(CreateBusinessRequest req, CancellationToken ct)
    {
        var newBusiness = new Business
        {
            Name = req.Name,
        };
        await repo.InsertAsync(newBusiness);
        logger.LogInformation("Event created: {Name}", newBusiness.Name);
        await SendOkAsync(ct);
    }
}