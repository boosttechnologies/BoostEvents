using BoostEvents.Web.Application.Interfaces;
using BoostEvents.Web.Domain;
using BoostEvents.Web.Features.SaleItems.Requests;
using FastEndpoints;

namespace BoostEvents.Web.Features.SaleItems;
public class CreateSaleItemEndpoint(ISaleItemRepo _repo, ILogger<CreateSaleItemEndpoint> _logger)
    : Endpoint<CreateSaleItemRequest, SaleItem>
{
    public override void Configure()
    {
        Post("/saleitems");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateSaleItemRequest req, CancellationToken ct)
    {
        var saleItem = new SaleItem
        {
            Name = req.Name,
            Description = req.Description,
        };

        _logger.LogInformation("Creating SaleItem: {@SaleItem}", saleItem);
        await _repo.CreateAsync(saleItem);
        await SendAsync(saleItem, cancellation: ct);
    }
}
