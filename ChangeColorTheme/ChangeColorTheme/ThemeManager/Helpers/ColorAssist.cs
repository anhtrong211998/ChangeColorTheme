using ChangeColorTheme.ThemeManager.Constants;
using ChangeColorTheme.ThemeManager.Extensions;
using ChangeColorTheme.ThemeManager.Structs;
using System;
using System.Windows.Media;

namespace ChangeColorTheme.ThemeManager.Helpers
{
    public static class ColorAssist
    {
        public static Color ContrastingForegroundColor(this Color color)
            => color.IsLightColor() ? Colors.Black : Colors.White;

        public static bool IsLightColor(this Color color)
        {
            double rgb_srgb(double d)
            {
                d /= 255.0;
                return (d > 0.03928)
                    ? Math.Pow((d + 0.055) / 1.055, 2.4)
                    : d / 12.92;
            }
            var r = rgb_srgb(color.R);
            var g = rgb_srgb(color.G);
            var b = rgb_srgb(color.B);

            var luminance = 0.2126 * r + 0.7152 * g + 0.0722 * b;
            return luminance > 0.179;
        }

        public static bool IsDarkColor(this Color color) => !IsLightColor(color);

        public static Color Darken(this Color color, int amount = 1) => color.ShiftLightness(amount);

        public static Color Lighten(this Color color, int amount = 1) => color.ShiftLightness(-amount);

        public static Color ShiftLightness(this Color color, double amount = 1.0f)
        {
            var lab = color.ToLab();
            var shifted = new Lab(lab.L - LabConstants.Kn * amount, lab.A, lab.B);
            return shifted.ToColor();
        }

        public static Color ShiftLightness(this Color color, int amount = 1)
        {
            var lab = color.ToLab();
            var shifted = new Lab(lab.L - LabConstants.Kn * amount, lab.A, lab.B);
            return shifted.ToColor();
        }
    }
}
