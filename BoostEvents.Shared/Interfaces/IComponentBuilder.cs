using BoostEvents.Shared.Components;
using BoostEvents.Shared.Layout;
using BoostEvents.Shared.Models;
using BoostEvents.Shared.Services;

namespace BoostEvents.Shared.Interfaces;

public interface IComponentBuilder
{
    Dictionary<string, string> BuildButtonCSS(string size, ComponentStyle.ColorScheme color, Button.ButtonDropdownPosition position);
    
    string BuildIndicatorCss(ComponentStyle.ColorScheme color);
    
    string BuildBadgeCss(ComponentStyle.ColorScheme color);
    
    Dictionary<string, string> BuildMainCSS(Main.DetailsLayout detailsLayout, bool detailsOpen);

    public static class Styles
    {
    }
}