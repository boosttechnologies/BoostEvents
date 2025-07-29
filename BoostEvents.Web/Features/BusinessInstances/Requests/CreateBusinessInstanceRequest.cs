namespace BoostEvents.Web.Features.BusinessInstances.Requests;

public class CreateBusinessInstanceRequest
{
    public string Name { get; set; } = default!;
    public Guid BusinessId { get; set; }
}