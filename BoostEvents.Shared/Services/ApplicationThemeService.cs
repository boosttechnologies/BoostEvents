using BoostEvents.Shared.Interfaces;
using BoostEvents.Shared.Layout;
using BoostEvents.Shared.Models;

namespace BoostEvents.Shared.Services;

public class ApplicationThemeService : IApplicationTheme
{
    public ApplicationTheme Settings { get; set; } = new();
    public bool DetailsOpen { get; set; }
    public Main.DetailsLayout DetailsLayout { get; set; } = Main.DetailsLayout.Closed;

    public event Action OnChange = null!;

    // Method to trigger the event
    public void NotifyStateChanged()
    {
        OnChange?.Invoke(); // Raise the event
    }
    
    public void ToggleColorScheme()
    {
        Settings.Mode = Settings.Mode switch
        {
            ApplicationTheme.ColorMode.Dark => ApplicationTheme.ColorMode.Light,
            ApplicationTheme.ColorMode.Light => ApplicationTheme.ColorMode.Dark,
            _ => Settings.Mode
        };
        NotifyStateChanged();
    }
    
}