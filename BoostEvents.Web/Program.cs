using BoostEvents.Shared.Interfaces;
using BoostEvents.Shared.Models;
using BoostEvents.Web.Components;
using BoostEvents.Shared.Services;
using BoostEvents.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Add device-specific services used by the BoostEvents.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddSingleton<IButtonService, ButtonService>();
builder.Services.AddSingleton<IApplicationTheme, ApplicationThemeService>();
builder.Services.AddSingleton<IComponentBuilder, ComponentBuilderService>();


builder.Services.AddLogging(logging =>
{
    logging.SetMinimumLevel(LogLevel.Information);  // Set appropriate log level
    logging.AddConsole();  // Optional: Adds console logging in dev mode
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(
        typeof(BoostEvents.Shared._Imports).Assembly,
        typeof(BoostEvents.Web.Client._Imports).Assembly);

app.Run();
