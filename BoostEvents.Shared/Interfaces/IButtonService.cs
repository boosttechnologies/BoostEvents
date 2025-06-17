using BoostEvents.Shared.Components;
using BoostEvents.Shared.Models;

namespace BoostEvents.Shared.Interfaces;

public interface IButtonService
{
    Task HandleButtonClick(Button button);

    Dictionary<string, string> GetStyles(string size, ComponentStyle.ColorScheme color, Button.ButtonDropdownPosition position);
}