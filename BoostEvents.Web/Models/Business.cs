namespace BoostEvents.Web.Models;

public class Business
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;
    public required string Name { get; set; }
}