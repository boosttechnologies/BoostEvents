namespace BoostEvents.Web.Infrastructure.Db;

public class TenantInfo : ITenantInfo
{
    public Guid TenantId { get; private set; }

    public TenantInfo(IHttpContextAccessor httpAccessor)
    {
        // TODO how do we handle actual Tenants
        /*var header = httpAccessor.HttpContext?.Request.Headers["X-Tenant-ID"].FirstOrDefault();
        if (Guid.TryParse(header, out var tid))
            TenantId = tid;
        else
            throw new UnauthorizedAccessException("Invalid or missing tenant ID");*/

        TenantId = new Guid("1e9568be-71d1-4b11-ae92-b3030168629c");
    }
}