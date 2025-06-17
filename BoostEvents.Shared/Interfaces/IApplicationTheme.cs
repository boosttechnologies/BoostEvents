using BoostEvents.Shared.Layout;
using BoostEvents.Shared.Models;

namespace BoostEvents.Shared.Interfaces;

public interface IApplicationTheme
{
    ApplicationTheme Settings { get; set; }
    
    bool DetailsOpen { get; set; }
    Main.DetailsLayout DetailsLayout { get; set; }
    
    event Action OnChange;
    void NotifyStateChanged();
    void ToggleColorScheme();
    void ToggleDetails(Main.DetailsLayout layout);

}
