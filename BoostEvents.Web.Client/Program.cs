using BoostEvents.Shared.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BoostEvents.Shared.Services;
using BoostEvents.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the BoostEvents.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddSingleton<IButtonService, ButtonService>();
builder.Services.AddSingleton<IApplicationTheme, ApplicationThemeService>();
builder.Services.AddSingleton<IComponentBuilder, ComponentBuilderService>();

await builder.Build().RunAsync();
