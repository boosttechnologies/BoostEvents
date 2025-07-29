using System.Net;
using System.Text.Json;

namespace BoostEvents.Web.Infrastructure.Extensions;

public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
{
    private readonly ILogger _logger = logger;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var traceId = context.TraceIdentifier;
            _logger.LogError(ex, "Unhandled exception occurred. TraceId: {TraceId}", traceId);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new
            {
                error = "An unexpected error occurred.",
                traceId
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}