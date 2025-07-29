using BoostEvents.Web.Application.Interfaces;

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

        TenantId = new Guid("2d837758-d6a7-4cb9-8a6a-b3290135fd8a");
    }
}