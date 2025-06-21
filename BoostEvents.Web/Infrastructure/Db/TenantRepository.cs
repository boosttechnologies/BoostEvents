using System.Data;
using BoostEvents.Web.Models;
using Dapper;

namespace BoostEvents.Web.Infrastructure.Db;

public class TenantRepository(IDbConnection _db, IBoostIdGenerator _boostIdGenerator) : ITenantRepository
{
    public Task InsertAsync(Tenant tenant)
    {
        var sql = "INSERT INTO tenants (id, name) VALUES (@Id, @Name)";
        tenant.Id = _boostIdGenerator.New();
        return _db.ExecuteAsync(sql, tenant);
    }
}