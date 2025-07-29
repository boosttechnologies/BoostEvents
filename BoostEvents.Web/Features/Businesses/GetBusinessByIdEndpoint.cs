using BoostEvents.Web.Application.Interfaces;
using BoostEvents.Web.Domain;
using BoostEvents.Web.Infrastructure.Db;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace BoostEvents.Web.Features.Businesses;

public class GetBusinessByIdRequest
{
    [FromRoute]
    public Guid Id { get; set; }
}

public class GetBusinessByIdEndpoint(IBusinessRepo repo, ILogger<GetBusinessByIdEndpoint> logger)
    : EndpointWithoutRequest<Business>
{
    public override void Configure()
    {
        Get("/businesses/{id:guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        logger.LogInformation("Fetching business by ID: {Id}", id);

        var result = await repo.ReadByIdAsync(id);
        if (result is null)
            await SendNotFoundAsync(ct);
        else
            await SendAsync(result, cancellation: ct);
    }
}