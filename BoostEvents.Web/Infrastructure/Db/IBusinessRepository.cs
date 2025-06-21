using BoostEvents.Web.Models;

namespace BoostEvents.Web.Infrastructure.Db;

public interface IBusinessRepository
{
    Task<IEnumerable<Business>> GetAllAsync();
    Task<Business?> GetByIdAsync(Guid id);
    Task InsertAsync(Business e);
}