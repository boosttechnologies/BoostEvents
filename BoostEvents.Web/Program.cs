using BoostEvents.Web;
using BoostEvents.Web.Components;
using BoostEvents.Web.Infrastructure.Extensions;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/bootstrap.log", rollingInterval: RollingInterval.Day)
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

// 🔧 Use Serilog after bootstrap
builder.Host.UseSerilog((ctx, services, config) =>
{
    config.ReadFrom.Configuration(ctx.Configuration)
          .ReadFrom.Services(services)
          .Enrich.FromLogContext();
});

// TODO CORS for Render + optional frontend
/*builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultPolicy", policy =>
    {
        policy.WithOrigins("https://yourdomain.com", "https://yourapp.onrender.com")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});*/

// Razor + Blazor hybrid
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Core Boost setup
builder.Services.AddBoostEventsCoreUi();

builder.Services.AddBoostEventsDataAccess(builder.Configuration);
builder.Services.AddBoostEventsCoreApi();

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("/app/dataprotection-keys"))
    .SetApplicationName("BoostEvents");

var app = builder.Build();

// TODO Reverse proxy compatibility (Render)
/*app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});*/

// Enforce HTTPS redirect (Render terminates TLS)
app.UseHttpsRedirection();

// TODO Use CORS policy (needed for web clients)
/*app.UseCors("DefaultPolicy");*/

// Cache static files (for Blazor assets)
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=604800");
    }
});

app.UseAntiforgery();

// Endpoint routing
app.UseFastEndpoints();

// Error handling
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.MapOpenApi();
    app.UseSwaggerGen();
    app.MapScalarApiReference();
}
else
{
    app.MapOpenApi(); // /openapi/v1.json
    app.UseSwaggerGen(); // /swagger
    app.MapScalarApiReference(); // /scalar/v1
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}



app.MapStaticAssets();

// Blazor hybrid UI
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(
        typeof(BoostEvents.Shared._Imports).Assembly,
        typeof(BoostEvents.Web.Client._Imports).Assembly);

app.Run();