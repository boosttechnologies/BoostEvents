using System.Data;
using BoostEvents.Web.Application.Interfaces;
using BoostEvents.Web.Domain;
using Dapper;

namespace BoostEvents.Web.Infrastructure.Db;

public class TenantRepository(IDbConnection _db, IBoostIdGenerator _boostIdGenerator) : ITenantRepository
{
    public Task InsertAsync(Tenant tenant)
    {
        const string sql = "INSERT INTO tenants (id, name, slug, created_utc, is_active, is_deleted) VALUES (@Id, @Name, @Slug, @CreatedUtc, @IsActive, @IsDeleted);";
        tenant.Id = _boostIdGenerator.New();
        tenant.Slug = tenant.Name; // todo -> Create unique slug
        tenant.CreatedUtc = DateTime.UtcNow;
        tenant.IsActive = true;
        tenant.IsDeleted = false;
        return _db.ExecuteAsync(sql, tenant);
    }
}