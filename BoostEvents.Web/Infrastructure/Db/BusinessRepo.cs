using System.Data;
using BoostEvents.Web.Application.Interfaces;
using BoostEvents.Web.Domain;
using Dapper;

namespace BoostEvents.Web.Infrastructure.Db;

public class BusinessRepo(IDbConnection _db, ITenantInfo _tenant, IBoostIdGenerator _boostIdGenerator, ILogger<BusinessRepo> _logger) : IBusinessRepo
{
    public async Task CreateAsync(Business b)
    {
        try
        {
            b.Id = Guid.NewGuid();
            b.TenantId = _tenant.TenantId;
            b.CreatedUtc = DateTime.UtcNow;
            b.IsDeleted = false;

            const string sql = @"INSERT INTO businesses (id, tenant_id, name, created_utc, is_deleted)
                             VALUES (@Id, @TenantId, @Name, @CreatedUtc, @IsDeleted);";
            
            var rowsAffected = await _db.ExecuteAsync(sql, b);

            if (rowsAffected == 0)
               _logger.LogError("Creating business failed: {@Business}", b);
            else
               _logger.LogInformation("Created business successfully: {@Business}", b);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating business: {@Business}", b);
        }
    }

    public async Task<IEnumerable<Business>> ReadAsync(Guid tenantId)
    {
        try
        {
            const string sql = "SELECT * FROM businesses WHERE tenant_id = @TenantId AND is_deleted = false ORDER BY created_utc DESC";
            _logger.LogInformation("Reading businesses for TenantId: {TenantId}", tenantId);
            return await _db.QueryAsync<Business>(sql, new { TenantId = tenantId });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading all businesses by tenant.");
            return [];
        }
    }

    public async Task<Business?> ReadByIdAsync(Guid id)
    {
        try
        {
            const string sql = "SELECT * FROM businesses WHERE id = @Id AND is_deleted = false";
            _logger.LogInformation("Reading business by Id: {BusinessId}", id);
            return await _db.QueryFirstOrDefaultAsync<Business>(sql, new { Id = id });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading business by Id: {BusinessId}", id);
            return null;
        }
    }
    
    public async Task UpdateAsync(Business b)
    {
        try
        {
            const string sql = """
                               UPDATE businesses
                               SET name = @Name
                               WHERE id = @Id AND tenant_id = @TenantId AND is_deleted = false;
                               """;

            if (b.TenantId == Guid.Empty)
                b.TenantId = _tenant.TenantId;

            var rowsAffected = await _db.ExecuteAsync(sql, new { b.Name, b.Id, b.TenantId });
            if (rowsAffected == 0)
                _logger.LogWarning("No business found to update. ID: {Id}, TenantId: {TenantId}", b.Id, b.TenantId);
            else
                _logger.LogInformation("Updating business: {@Business}", b);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error updating business: {@Business}", b);
        }
        
    }

    public async Task SoftDeleteAsync(Guid id)
    {
        try
        {
            var deleteTime = DateTime.UtcNow;
            
            const string sql = """
                               UPDATE businesses
                               SET is_deleted = true,
                                   deleted_utc = @DeleteTime
                               WHERE id = @Id
                                 AND tenant_id = @TenantId
                                 AND is_deleted = false;
                               """;
            
            var rowsAffected = await _db.ExecuteAsync(sql, new { Id = id, DeleteTime = deleteTime, _tenant.TenantId });
            if (rowsAffected == 0)
                _logger.LogWarning("No business found to soft delete. ID: {Id}", id);
            else
                _logger.LogInformation("Soft delete business completed ID: {BusinessId}", id);
            
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error soft deleting business: {@BusinessId}", id);
        }
    }
}