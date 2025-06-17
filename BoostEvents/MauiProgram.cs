using Microsoft.Extensions.Logging;
using BoostEvents.Shared.Services;
using BoostEvents.Services;
using BoostEvents.Shared.Interfaces;

namespace BoostEvents;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // Add device-specific services used by the BoostEvents.Shared project
        builder.Services.AddSingleton<IFormFactor, FormFactor>();
        builder.Services.AddSingleton<IButtonService, ButtonService>();
        builder.Services.AddSingleton<IApplicationTheme, ApplicationThemeService>();
        builder.Services.AddSingleton<IComponentBuilder, ComponentBuilderService>();
        builder.Services.AddLogging(logging =>
        {
            logging.SetMinimumLevel(LogLevel.Information);  // Set appropriate log level
        });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
