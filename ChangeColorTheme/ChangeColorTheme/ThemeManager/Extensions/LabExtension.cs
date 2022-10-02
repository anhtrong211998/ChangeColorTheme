﻿using ChangeColorTheme.ThemeManager.Constants;
using ChangeColorTheme.ThemeManager.Structs;
using System;
using System.Windows.Media;

namespace ChangeColorTheme.ThemeManager.Extensions
{
    public static class LabExtension
    {
        public static Lab ToLab(this Color c)
        {
            var xyz = c.ToXyz();
            return xyz.ToLab();
        }

        public static Lab ToLab(this Xyz xyz)
        {
            double xyz_lab(double v)
            {
                if (v > LabConstants.e)
                    return Math.Pow(v, 1 / 3.0);
                else
                    return (v * LabConstants.k + 16) / 116;
            }

            var fx = xyz_lab(xyz.X / LabConstants.WhitePointX);
            var fy = xyz_lab(xyz.Y / LabConstants.WhitePointY);
            var fz = xyz_lab(xyz.Z / LabConstants.WhitePointZ);

            var l = 116 * fy - 16;
            var a = 500 * (fx - fy);
            var b = 200 * (fy - fz);
            return new Lab(l, a, b);
        }

        public static Color ToColor(this Lab lab)
        {
            var xyz = lab.ToXyz();

            return xyz.ToColor();
        }

        public static Color ToColor(this Xyz xyz)
        {
            double xyz_rgb(double d)
            {
                if (d > 0.0031308)
                    return 255.0 * (1.055 * Math.Pow(d, 1.0 / 2.4) - 0.055);
                else
                    return 255.0 * (12.92 * d);
            }

            byte clip(double d)
            {
                if (d < 0) return 0;
                if (d > 255) return 255;
                return (byte)Math.Round(d);
            }
            var r = xyz_rgb(3.2404542 * xyz.X - 1.5371385 * xyz.Y - 0.4985314 * xyz.Z);
            var g = xyz_rgb(-0.9692660 * xyz.X + 1.8760108 * xyz.Y + 0.0415560 * xyz.Z);
            var b = xyz_rgb(0.0556434 * xyz.X - 0.2040259 * xyz.Y + 1.0572252 * xyz.Z);

            return Color.FromRgb(clip(r), clip(g), clip(b));
        }
        public static Xyz ToXyz(this Color c)
        {
            double rgb_xyz(double v)
            {
                v /= 255;
                if (v > 0.04045)
                    return Math.Pow((v + 0.055) / 1.055, 2.4);
                else
                    return v / 12.92;
            }

            var r = rgb_xyz(c.R);
            var g = rgb_xyz(c.G);
            var b = rgb_xyz(c.B);

            var x = 0.4124564 * r + 0.3575761 * g + 0.1804375 * b;
            var y = 0.2126729 * r + 0.7151522 * g + 0.0721750 * b;
            var z = 0.0193339 * r + 0.1191920 * g + 0.9503041 * b;
            return new Xyz(x, y, z);
        }

        public static Xyz ToXyz(this Lab lab)
        {
            double lab_xyz(double d)
            {
                if (d > LabConstants.eCubedRoot)
                    return d * d * d;
                else
                    return (116 * d - 16) / LabConstants.k;
            }

            var y = (lab.L + 16.0) / 116.0;
            var x = double.IsNaN(lab.A) ? y : y + lab.A / 500.0;
            var z = double.IsNaN(lab.B) ? y : y - lab.B / 200.0;

            y = LabConstants.WhitePointY * lab_xyz(y);
            x = LabConstants.WhitePointX * lab_xyz(x);
            z = LabConstants.WhitePointZ * lab_xyz(z);

            return new Xyz(x, y, z);
        }
    }
}