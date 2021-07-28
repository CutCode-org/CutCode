using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CutCode
{
    public class HomeViewModel : Screen
    {
        private readonly IThemeService themeService;
        public HomeViewModel(IThemeService _themeService)
        {
            themeService = _themeService;
            themeService.ThemeChanged += ThemeChanged;
            SetAppearance();
        }
        private void ThemeChanged(object sender, EventArgs e)
        {
            SetAppearance();
        }

        private void SetAppearance()
        {
            Theme = themeService.IsLightTheme;
            searchBarBackground = themeService.IsLightTheme ? (Color)ColorConverter.ConvertFromString("#DADBDC") : (Color)ColorConverter.ConvertFromString("#2A2E33");
            searchBarTextColor = themeService.IsLightTheme ? (Color)ColorConverter.ConvertFromString("#000000") : (Color)ColorConverter.ConvertFromString("#FFFFFF");
            searchBarHoverColor = themeService.IsLightTheme ? (Color)ColorConverter.ConvertFromString("#D0D1D2") : (Color)ColorConverter.ConvertFromString("#373737");
        }

        private Color _searchBarBackground;
        public Color searchBarBackground
        {
            get => _searchBarBackground;
            set =>SetAndNotify(ref _searchBarBackground, value);
            
        }

        private Color _searchBarHoverColor;
        public Color searchBarHoverColor
        {
            get => _searchBarHoverColor;
            set => SetAndNotify(ref _searchBarHoverColor, value);

        }

        private Color _searchBarTextColor;
        public Color searchBarTextColor
        {
            get => _searchBarTextColor;
            set =>  SetAndNotify(ref _searchBarTextColor, value);
            
        }

        private bool _Theme;
        public bool Theme
        {
            get => _Theme;
            set => SetAndNotify(ref _Theme, value);
            
        }
        public void SearchCommand()
        {
            // Search with the search text given
        }
    }
}
