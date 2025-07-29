using BoostEvents.Web.Domain;

namespace BoostEvents.Web.Application.Interfaces;

public interface ITenantRepository
{
    Task InsertAsync(Tenant tenant);
}