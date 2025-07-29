using System.Data;
using BoostEvents.Web.Application.Interfaces;
using BoostEvents.Web.Domain;
using Dapper;

namespace BoostEvents.Web.Infrastructure.Db;

public class SaleItemRepo(IDbConnection _db, IUserInfo _user, ILogger<SaleItemRepo> _logger, IBoostIdGenerator _boostIdGenerator) : ISaleItemRepo
{

    public async Task CreateAsync(SaleItem b)
    {
        try
        {
            b.Id = _boostIdGenerator.New();
            b.TenantId = _user.TenantId;
            b.BusinessInstanceId = _user.BusinessInstanceId;
            b.CreatedUtc = DateTime.UtcNow;
            b.IsDeleted = false;
            b.IsVisibleToPublic = false;
            
            const string sql = @"INSERT INTO sale_items (id, tenant_id, business_instance_id, name, created_utc, is_deleted, is_visible_to_public, description)
                             VALUES (@Id, @TenantId, @BusinessInstanceId, @Name, @CreatedUtc, @IsDeleted, @IsVisibleToPublic, @Description);";
            
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

    public Task<IEnumerable<SaleItem>> ReadAsync(Guid tenantId)
    {
        throw new NotImplementedException();
    }

    public Task<SaleItem?> ReadByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(SaleItem entity)
    {
        throw new NotImplementedException();
    }

    public Task SoftDeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    /*public async Task<IEnumerable<Business>> ReadAsync(Guid tenantId)
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
            const string sql = """
                               SELECT 
                                   id AS Id,
                                   tenant_id AS TenantId,
                                   name AS Name,
                                   modified_utc AS CreatedUtc
                               FROM businesses WHERE id = @Id AND is_deleted = false
                               """;
            
            _logger.LogInformation("Reading business by Id: {BusinessId}", id);
            var test = await _db.QueryFirstOrDefaultAsync<Business>(sql, new { Id = id });
            return test;
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
                               SET name = @Name, modified_utc = @ModifiedUtc
                               WHERE id = @Id AND tenant_id = @TenantId AND is_deleted = false;
                               """;

            if (b.TenantId == Guid.Empty)
                b.TenantId = user.TenantId;

            var rowsAffected = await _db.ExecuteAsync(sql, new { b.Name, b.Id, b.TenantId, b.ModifiedUtc });
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
            
            var rowsAffected = await _db.ExecuteAsync(sql, new { Id = id, DeleteTime = deleteTime, user.TenantId });
            if (rowsAffected == 0)
                _logger.LogWarning("No business found to soft delete. ID: {Id}", id);
            else
                _logger.LogInformation("Soft delete business completed ID: {BusinessId}", id);
            
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error soft deleting business: {@BusinessId}", id);
        }
    }*/
    
    
}