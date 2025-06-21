using BoostEvents.Web.Models;

namespace BoostEvents.Web.Infrastructure.Db;

public interface ITenantRepository
{
    Task InsertAsync(Tenant tenant);
}