using System;

namespace ChangeColorTheme.ThemeManager.Enum
{
    [Flags]
    public enum ColorSelection
    {
        None = 0,
        Primary = 1,
        Secondary = 2,
        All = Primary | Secondary
    }
}
