using ChangeColorTheme.ThemeManager.Helpers;
using System.Windows.Media;

namespace ChangeColorTheme.ThemeManager.Structs
{
    public struct ColorPair
    {
        public Color Color { get; set; }

        public Color? ForegroundColor { get; set; }

        public static implicit operator ColorPair(Color color) => new ColorPair(color);

        public ColorPair(Color color)
        {
            Color = color;
            ForegroundColor = null;
        }

        public ColorPair(Color color, Color? foreground)
        {
            Color = color;
            ForegroundColor = foreground;
        }

        public Color GetForegroundColor() => ForegroundColor ?? Color.ContrastingForegroundColor();
    }
}
