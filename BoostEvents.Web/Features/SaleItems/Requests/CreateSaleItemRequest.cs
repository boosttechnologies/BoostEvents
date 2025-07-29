namespace BoostEvents.Web.Features.SaleItems.Requests;

public class CreateSaleItemRequest
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}