namespace BoostEvents.Shared.Models;

public class ApplicationTheme
{
    public ColorMode Mode { get; set; } = ColorMode.Dark;
    public ThemeColors Colors { get; set; } = new();

    public class ThemeColors()
    {
        public string DarkBackground { get; set; } = "DarkBackground";
        public string DarkBody { get; set; } = "DarkBody";
        public string DarkBorder { get; set; } = "white/10";
    
        public string LightBackground { get; set; } = "LightBackground";
        public string LightBody { get; set; } = "LightBody";
        public string LightBorder { get; set; } = "zinc-950/15";
    }
    
    public enum ColorMode
    {
        Dark,
        Light
    }
}