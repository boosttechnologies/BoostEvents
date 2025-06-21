using BoostEvents.Shared.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BoostEvents.Shared.Services;
using BoostEvents.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

/*builder.Services.AddScoped<ITenantInfo, TenantInfo>();*/

// Add device-specific services used by the BoostEvents.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddSingleton<IButtonService, ButtonService>();
builder.Services.AddSingleton<IApplicationTheme, ApplicationThemeService>();
builder.Services.AddSingleton<IComponentBuilder, ComponentBuilderService>();

builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddSingleton<IApplicationTheme, ApplicationThemeService>();
        
// Components
builder.Services.AddSingleton<IComponentBuilder, ComponentBuilderService>();
builder.Services.AddSingleton<IButtonService, ButtonService>();



builder.Services.AddHttpClient("BoostApi", client =>
{
    client.BaseAddress = new Uri("http://127.0.0.1:8080/api/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});


await builder.Build().RunAsync();
