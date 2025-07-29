using BoostEvents.Web.Application.Interfaces;
using Guid = System.Guid;

namespace BoostEvents.Web.Infrastructure.Db;

public class UserInfo : IUserInfo
{
    public string Auth0UserId { get; set; }
    public Guid TenantId { get; private set; }
    public Guid BusinessId { get; private set; }
    public Guid BusinessInstanceId { get; private set; }

    public UserInfo(IHttpContextAccessor _httpAccessor)
    {
        // TODO how do we handle actual Tenants
        /*var header = httpAccessor.HttpContext?.Request.Headers["X-Tenant-ID"].FirstOrDefault();
        if (Guid.TryParse(header, out var tid)) 
            TenantId = tid;
        else
            throw new UnauthorizedAccessException("Invalid or missing tenant ID");*/
        
        // Three Key Items the entire app needs to know to function
        Auth0UserId = "Auth0 - 1213456";
        TenantId = new Guid("2d837758-d6a7-4cb9-8a6a-b3290135fd8a");
        BusinessId = new Guid("8698c671-d699-48ad-8826-b3290166896a");
        BusinessInstanceId = new Guid("343e0dc9-fc4f-4896-9ff5-b3290166d155");
    }
}