using BoostEvents.Web.Application.Interfaces;
using BoostEvents.Web.Infrastructure.Db;
using Serilog.Context;

namespace BoostEvents.Web.Infrastructure.Extensions;

public class SerilogEnrichmentMiddleware(
    IHttpContextAccessor httpContextAccessor,
    ITenantInfo tenantInfo)
    : IMiddleware
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var tenantId = tenantInfo.TenantId.ToString();
        var traceId = context.TraceIdentifier;

        LogContext.PushProperty("TenantId", tenantId);
        /*LogContext.PushProperty("UserId", userId);*/
        LogContext.PushProperty("TraceId", traceId);

        await next(context);
    }
}