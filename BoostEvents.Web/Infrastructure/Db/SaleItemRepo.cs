using System.Data;
using BoostEvents.Web.Application.Interfaces;
using BoostEvents.Web.Domain;
using Dapper;

namespace BoostEvents.Web.Infrastructure.Db;

public class SaleItemRepo(IDbConnection db, ITenantInfo _tenant, ILogger<SaleItemRepo> logger) : ISaleItemRepo
{
    public async Task CreateAsync(SaleItem saleItem)
    {
        saleItem.Id = Guid.NewGuid();
        saleItem.TenantId = _tenant.TenantId;
        saleItem.Description = "";
        saleItem.CreatedUtc = DateTime.UtcNow;
        saleItem.IsDeleted = false;

        const string sql = """
            INSERT INTO sale_items (id, tenant_id, name, description, is_deleted, created_utc)
            VALUES (@Id, @TenantId, @Name, @Description,@IsDeleted, @CreatedUtc)
        """;

        await db.ExecuteAsync(sql, saleItem);
        logger.LogInformation("SaleItem created with ID {Id}", saleItem.Id);
    }

    public async Task<IEnumerable<SaleItem>> ReadAsync(Guid tenantId)
    {
        const string sql = "SELECT * FROM sale_items WHERE tenant_id = @TenantId AND is_deleted = false";
        return await db.QueryAsync<SaleItem>(sql, new { _tenant.TenantId });
    }

    public async Task<SaleItem?> ReadByIdAsync(Guid id)
    {
        const string sql = "SELECT * FROM sale_items WHERE id = @Id AND tenant_id = @TenantId AND is_deleted = false";
        return await db.QuerySingleOrDefaultAsync<SaleItem>(sql, new { Id = id, _tenant.TenantId });
    }

    public async Task UpdateAsync(SaleItem saleItem)
    {
        /*const string sql = """
            UPDATE sale_items SET name = @Name, description = @Description, price = @Price, sale_type = @SaleType,
            time_slot_start_utc = @TimeSlotStartUtc, time_slot_end_utc = @TimeSlotEndUtc, parent_id = @ParentId
            WHERE id = @Id AND tenant_id = @TenantId AND is_deleted = false
        """;

        await db.ExecuteAsync(sql, saleItem);*/
        logger.LogInformation("SaleItem updated: {Id}", saleItem.Id);
    }

    public async Task SoftDeleteAsync(Guid id)
    {
        const string sql = """
            UPDATE sale_items SET is_deleted = true, deleted_utc = @Now
            WHERE id = @Id AND tenant_id = @TenantId AND is_deleted = false
        """;

        await db.ExecuteAsync(sql, new { Id = id, _tenant.TenantId, Now = DateTime.UtcNow });
        logger.LogInformation("SaleItem soft-deleted: {Id}", id);
    }
}