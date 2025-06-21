namespace BoostEvents.Web.Infrastructure.Db;

public interface ITenantInfo
{
    Guid TenantId { get; }
}