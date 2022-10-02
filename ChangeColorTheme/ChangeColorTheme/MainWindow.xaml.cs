using ChangeColorTheme.ThemeManager;
using ChangeColorTheme.ThemeManager.Extensions;
using ChangeColorTheme.ThemeManager.Helpers;
using System.Windows;
using System.Windows.Media;

namespace ChangeColorTheme
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly PaletteHelper _paletteHelper = new PaletteHelper();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            ITheme theme = _paletteHelper.GetTheme();
            IBaseTheme baseTheme = new MaterialDesignDarkTheme();
            theme.SetBaseTheme(baseTheme);
            _paletteHelper.SetTheme(theme);
        }

        private void Yellow_Click(object sender, RoutedEventArgs e)
        {
            ChangeCustomColor(Colors.Yellow);
        }

        private void Blue_Click(object sender, RoutedEventArgs e)
        {
            ChangeCustomColor(Colors.Blue);
        }

        private void ChangeCustomColor(object? obj)
        {
            var color = (Color)obj!;
            _paletteHelper.ChangePrimaryColor(color);
        }

        private void dark_light_Unchecked(object sender, RoutedEventArgs e)
        {
            ITheme theme = _paletteHelper.GetTheme();
            IBaseTheme baseTheme = new MaterialDesignLightTheme();
            theme.SetBaseTheme(baseTheme);
            _paletteHelper.SetTheme(theme);
        }
    }
}
