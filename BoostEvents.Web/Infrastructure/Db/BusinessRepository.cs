using System.Data;
using BoostEvents.Web.Models;
using Dapper;

namespace BoostEvents.Web.Infrastructure.Db;

public class BusinessRepository(IDbConnection _db, ITenantInfo _tenant, IBoostIdGenerator _boostIdGenerator) : IBusinessRepository
{
    public Task<IEnumerable<Business>> GetAllAsync()
    {
        var sql = "SELECT * FROM Business WHERE tenant_id = @TenantId";
        return _db.QueryAsync<Business>(sql, new { TenantId = _tenant.TenantId });
    }

    public Task<Business?> GetByIdAsync(Guid id)
    {
        var sql = "SELECT * FROM Business WHERE id = @Id AND tenant_id = @TenantId";
        return _db.QuerySingleOrDefaultAsync<Business>(sql, new { Id = id, TenantId = _tenant.TenantId });
    }

    public Task InsertAsync(Business b)
    {
        var sql = "INSERT INTO business (id, tenant_id, name) VALUES (@Id, @TenantId, @Name)";
        b.Id = _boostIdGenerator.New();
        b.TenantId = _tenant.TenantId;
        return _db.ExecuteAsync(sql, b);
    }
}