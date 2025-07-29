using System.Data;
using BoostEvents.Web.Application.Interfaces;
using BoostEvents.Web.Domain;
using Dapper;

namespace BoostEvents.Web.Infrastructure.Db;

public class BusinessInstanceRepo(
    IDbConnection _db,
    IUserInfo user,
    IBoostIdGenerator _boostIdGenerator,
    ILogger<BusinessInstanceRepo> _logger
) : IBusinessInstanceRepo
{
    public async Task CreateAsync(BusinessInstance instance)
    {
        try
        {
            instance.Id = _boostIdGenerator.New();
            instance.TenantId = user.TenantId;
            instance.CreatedUtc = DateTime.UtcNow;
            instance.IsDeleted = false;
            instance.StartUtc = DateTime.UtcNow; // TODO -> User input
            instance.EndUtc = DateTime.UtcNow; // TODO -> User input

            const string sql = @"INSERT INTO business_instances 
            (id, tenant_id, business_id, name, created_utc, is_deleted, start_utc, end_utc, deleted_utc) 
            VALUES (@Id, @TenantId, @BusinessId, @Name, @CreatedUtc, @IsDeleted, @StartUtc, @EndUtc, @DeletedUtc);";

            _logger.LogInformation("Creating business instance: {@Instance}", instance);
            await _db.ExecuteAsync(sql, instance);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to create business instance. {@Instance}", instance);
        }
    }

    public async Task<BusinessInstance?> ReadByIdAsync(Guid id)
    {
        try
        {
            const string sql = """
                               SELECT 
                                   id AS Id,
                                   name AS Name,
                                   tenant_id AS TenantId,
                                   business_id AS BusinessId,
                                   start_utc AS StartUtc,
                                   end_utc AS EndUtc,
                                   created_utc AS CreatedUtc,
                                   deleted_utc AS DeletedUtc
                               FROM business_instances
                               WHERE id = @Id AND tenant_id = @TenantId AND is_deleted = false
                               """;    
        
            return await _db.QuerySingleOrDefaultAsync<BusinessInstance>(sql, new { Id = id, TenantId = user.TenantId });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to read business instance ID: {@Id}", id);
            return null;
        }
    }

    public async Task<IEnumerable<BusinessInstance>> ReadAsync(Guid tenantId)
    {

        try
        {
            const string sql = """
                               SELECT * 
                               FROM business_instances 
                               WHERE tenant_id = @TenantId AND is_deleted = false
                               """;

            return await _db.QueryAsync<BusinessInstance>(sql, new { TenantId = user.TenantId });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to read business instance ID: {@TenantId}", tenantId);
            return [];
        }
    }

    public async Task UpdateAsync(BusinessInstance instance)
    {
        try
        {
            instance.ModifiedUtc = DateTime.UtcNow;
            _logger.LogInformation("Updating business instance: {@Instance}", instance);
        
            const string sql = """
                               UPDATE business_instances 
                               SET name = @Name, modified_utc = @ModifiedUtc 
                               WHERE id = @Id AND tenant_id = @TenantId AND is_deleted = false
                               """;
        
            var rowsAffected = await _db.ExecuteAsync(sql, new
            {
                instance.Name, 
                instance.ModifiedUtc, 
                instance.Id, 
                instance.TenantId
            });

            if (rowsAffected == 0)
            {
                _logger.LogInformation("Business instance could not be updated: {@Instance}", instance);
            }
            else
            {
                _logger.LogInformation("Business instance updated: {@Instance}", instance);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to update business instance: {@Instance}", instance);
        }
    }

    public async Task SoftDeleteAsync(Guid id)
    {
        try
        {
            const string sql = """
                               UPDATE business_instances 
                               SET is_deleted = true, deleted_utc = @DeletedUtc 
                               WHERE id = @Id AND tenant_id = @TenantId
                               """;

            var deletedUtc = DateTime.UtcNow;
            _logger.LogInformation("Soft-deleting business instance: {Id} at {DeletedUtc}", id, deletedUtc);
            await _db.ExecuteAsync(sql, new { Id = id, TenantId = user.TenantId, DeletedUtc = deletedUtc });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to soft delete business instance: {@Id}", id);
        }
    }
}