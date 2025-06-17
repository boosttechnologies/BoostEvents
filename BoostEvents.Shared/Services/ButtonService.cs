using System.Text;
using BoostEvents.Shared.Components;
using BoostEvents.Shared.Interfaces;
using BoostEvents.Shared.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace BoostEvents.Shared.Services;

public class ButtonService(ILogger<ButtonService> logger) : IButtonService
{
    private readonly ILogger<ButtonService> _logger = logger;

    public async Task HandleButtonClick(Button button)
    {
        // Handle button-specific logic, such as tracking clicks or updating states
        await Task.CompletedTask;
    }

    public Dictionary<string, string> GetStyles(string size, ComponentStyle.ColorScheme color, Button.ButtonDropdownPosition position)
    {
        
        
        var buttonColor = color switch
        {
            ComponentStyle.ColorScheme.Primary => "bg-blue-500 hover:bg-blue-600 text-white",
            ComponentStyle.ColorScheme.Secondary => "bg-gray-500 hover:bg-gray-600 text-white",
            ComponentStyle.ColorScheme.Success => "bg-green-500 hover:bg-green-600 text-white",
            ComponentStyle.ColorScheme.Danger => "bg-red-500 hover:bg-red-600 text-white",
            ComponentStyle.ColorScheme.Warning => "bg-yellow-500 hover:bg-yellow-600 text-white",
            ComponentStyle.ColorScheme.Alternative => "bg-teal-500 hover:bg-teal-600 text-white",
            ComponentStyle.ColorScheme.Transparent => "dark:bg-transparent text-black hover:bg-zinc-900/10 dark:text-white dark:hover:bg-gray-800",
            ComponentStyle.ColorScheme.TransparentSecondary => "dark:bg-transparent text-black hover:text-white hover:bg-text-white hover:hover:bg-blue-800 dark:text-white dark:hover:bg-blue-800",
            _ => "bg-blue-500 hover:bg-blue-600 text-white"
        };
        
        // Drop Dowen Styles
        var dropdownClass = new StringBuilder("absolute z-10 w-48");
        var positionClass = position switch
        {
            Button.ButtonDropdownPosition.Top => "",
            Button.ButtonDropdownPosition.TopRight => "left-full top-0 ml-3",
            Button.ButtonDropdownPosition.Bottom => "top-full mt-3",
            Button.ButtonDropdownPosition.BottomRight => "left-full bottom-0 ml-3",
            Button.ButtonDropdownPosition.InsetBottomRight => "top-full right-0 mt-3",
            Button.ButtonDropdownPosition.Left => "",
            Button.ButtonDropdownPosition.Right => "left-full ml-3",
            _ => ""
            
            
        };
        dropdownClass.Append(" ").Append(positionClass).Append(" p-3");

        var sizeClass = size.ToLower() switch
        {
            "xsmall" => "text-sm font-medium py-1.5 px-2",
            "small" => "text-sm font-medium py-2 px-3",
            "large" => "text-lg font-medium py-4 px-6",
            _ => "text-sm font-medium p-3"
        };
        
        var buttonClasses = new StringBuilder("relative inline-block flex items-center justify-between gap-x-2.5 group cursor-pointer transition-all duration-300 ease-in-out font-body rounded-md text-zinc-950 dark:text-white").Append(" ").Append(buttonColor).Append(" ").Append(sizeClass).ToString();
        
        var styles = new Dictionary<string, string> { { "ButtonCss", buttonClasses }, { "DropdownCss", dropdownClass.ToString() }};

        return styles;
    }
}