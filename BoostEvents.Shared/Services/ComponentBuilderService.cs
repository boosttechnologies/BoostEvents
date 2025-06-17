using System.Text;
using BoostEvents.Shared.Components;
using BoostEvents.Shared.Interfaces;
using BoostEvents.Shared.Layout;
using BoostEvents.Shared.Models;

namespace BoostEvents.Shared.Services;

public class ComponentBuilderService : IComponentBuilder
{

    private readonly string Blue = "blue-500";
    private readonly string BlueDark = "blue-800";
    private readonly string BlueDarker = "blue-900";
    
    public Dictionary<string, string> BuildButtonCSS(string size, ComponentStyle.ColorScheme color, Button.ButtonDropdownPosition position)
    {
        var buttonColor = color switch
        {
            ComponentStyle.ColorScheme.Primary => $"bg-{BlueDark} hover:bg-{BlueDarker} text-white",
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
        
        var buttonClasses = new StringBuilder("relative inline-block flex items-center justify-between gap-x-2.5 group cursor-pointer transition-all duration-300 ease-in-out font-body rounded-md focus:outline-none focus:ring-2 focus:ring-offset-2 text-zinc-950 dark:text-white").Append(" ").Append(buttonColor).Append(" ").Append(sizeClass).ToString();
        
        var styles = new Dictionary<string, string> { { "ButtonCss", buttonClasses }, { "DropdownCss", dropdownClass.ToString() }};

        return styles;
    }
    
    public string BuildIndicatorCss(ComponentStyle.ColorScheme color)
    {
        var indicatorCss = color switch
        {
            ComponentStyle.ColorScheme.Primary => $"text-{Blue} bg-blue-100/10",
            ComponentStyle.ColorScheme.Secondary => "text-gray-500 bg-gray-100/10",
            ComponentStyle.ColorScheme.Success => "text-green-500 bg-green-100/10",
            ComponentStyle.ColorScheme.Danger => "text-red-500 bg-red-100/10",
            ComponentStyle.ColorScheme.Warning => "text-yellow-500 bg-yellow-100/10",
            ComponentStyle.ColorScheme.Alternative => "text-teal-500 bg-teal-100/10",
            ComponentStyle.ColorScheme.Transparent => "text-gray-500 bg-gray-100/10",
            _ => "text-blue-500 bg-blue-100/10"
        };
        return indicatorCss;
    }
    
    public string BuildBadgeCss(ComponentStyle.ColorScheme color)
    {
        var indicatorCss = color switch
        {
            ComponentStyle.ColorScheme.Primary => $"text-{Blue} bg-{Blue}/10 ring-{Blue}/30",
            ComponentStyle.ColorScheme.Secondary => "text-gray-400 bg-gray-400/10 ring-gray-400/20",
            ComponentStyle.ColorScheme.Success => "text-green-400 bg-green-400/10 ring-green-400/20",
            ComponentStyle.ColorScheme.Danger => "text-red-400 bg-red-400/10 ring-red-400/20",
            ComponentStyle.ColorScheme.Warning => "text-yellow-400 bg-yellow-400/10 ring-yellow-400/20",
            ComponentStyle.ColorScheme.Alternative => "text-teal-400 bg-teal-400/10 ring-teal-400/20",
            ComponentStyle.ColorScheme.Transparent => "text-gray-400 bg-gray-400/10 ring-gray-400/20",
            _ => "text-blue-400 bg-blue-400/10 ring-blue-400/20"
        };
        return indicatorCss;
    }
    
    public Dictionary<string, string> BuildMainCSS(Main.DetailsLayout layout, bool detailsIsOpen)
    {
        /*var indicatorCss = layout switch
        {
            case Main.DetailsLayout.Closed
                break;
            Main.DetailsLayout.Closed => $"text-{Blue} bg-{Blue}/10 ring-{Blue}/30",
            Main.DetailsLayout.Open => "text-gray-400 bg-gray-400/10 ring-gray-400/20",
            Main.DetailsLayout.OpenPopUp => "text-green-400 bg-green-400/10 ring-green-400/20",
            _ => "text-blue-400 bg-blue-400/10 ring-blue-400/20"
        };*/

        var mainCss = string.Empty;
        var mainDetailsCss = string.Empty;

        switch (layout)
        {
            case Main.DetailsLayout.Closed:
                mainCss = "";
                mainDetailsCss = "";
                break;
            case Main.DetailsLayout.Open:
                mainCss = "pr-[550px]";
                mainDetailsCss = "";
                break;
            case Main.DetailsLayout.OpenPopUp:
                mainCss = "";
                mainDetailsCss = "";
                break;
        }

        var styles = new Dictionary<string, string> { { "MainCss", mainCss }, { "MainDetailsCss", mainDetailsCss }};
        return styles;
    }
    
}