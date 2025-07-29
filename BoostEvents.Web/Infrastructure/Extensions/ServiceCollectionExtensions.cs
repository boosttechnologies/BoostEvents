using System.Data;
using BoostEvents.Shared.Interfaces;
using BoostEvents.Shared.Services;
using BoostEvents.Web.Application.Interfaces;
using BoostEvents.Web.Infrastructure.Db;
using BoostEvents.Web.Infrastructure.Persistence;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Npgsql;

namespace BoostEvents.Web.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBoostEventsCoreUi(this IServiceCollection services)
    {
        // Helpers
        services.AddSingleton<IFormFactor, FormFactor>();
        services.AddSingleton<IApplicationTheme, ApplicationThemeService>();
        services.AddSingleton<IBoostIdGenerator, BoostIdGenerator>();
        
        // Components
        services.AddSingleton<IComponentBuilder, ComponentBuilderService>();
        services.AddSingleton<IButtonService, ButtonService>();
       
        return services;
    }
    
    
    public static IServiceCollection AddBoostEventsDataAccess(this IServiceCollection services, IConfiguration config)
    {
        // PostgreSQL Connection Pool
        services.AddSingleton(sp =>
        {
            var connStr = config.GetConnectionString("DefaultConnection");
            Console.WriteLine("✅ [DEBUG] Connection string: " + (connStr ?? "[NULL]"));
            return new NpgsqlDataSourceBuilder(connStr).Build();
        });

        // Scoped Dapper Connection per Request
        services.AddScoped<IDbConnection>(sp =>
        {
            var ds = sp.GetRequiredService<NpgsqlDataSource>();
            var conn = ds.CreateConnection();
            conn.Open();
            return conn;
        });
        
        // Repositories (Dapper-based)
        services.AddScoped<IBusinessRepo, BusinessRepo>();
        services.AddScoped<ITenantRepository, TenantRepository>();
        
       services.AddDbContext<BoostEventsDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection")));
        
        return services;
    }
    public static IServiceCollection AddBoostEventsCoreApi(this IServiceCollection services)
    {
        // TODO - Work into code
        services.AddScoped<ITenantInfo, TenantInfo>();
        services.AddScoped<SerilogEnrichmentMiddleware>();
        services.AddFastEndpoints();
        services.SwaggerDocument(); // configured via appsettings.json (v6+)
        services.AddOpenApi(); // .NET 9’s built-in OpenAPI JSON support
        services.AddHttpContextAccessor();
        
        return services;
    }
    
}