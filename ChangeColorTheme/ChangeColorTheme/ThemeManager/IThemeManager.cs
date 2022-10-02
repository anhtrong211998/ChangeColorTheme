using ChangeColorTheme.ThemeManager.Events;
using System;

namespace ChangeColorTheme.ThemeManager
{
    public interface IThemeManager
    {
        event EventHandler<ThemeChangedEventArgs>? ThemeChanged;
    }
}
