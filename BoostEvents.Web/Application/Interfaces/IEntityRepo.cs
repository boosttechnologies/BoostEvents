namespace BoostEvents.Web.Application.Interfaces;

public interface IEntityRepo<T>
{
    Task CreateAsync(T entity);
    Task<IEnumerable<T>> ReadAsync(Guid tenantId);
    Task<T?> ReadByIdAsync(Guid id);
    Task UpdateAsync(T entity);
    Task SoftDeleteAsync(Guid id);
}