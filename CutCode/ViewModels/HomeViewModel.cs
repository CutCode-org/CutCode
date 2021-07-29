using Stylet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            IsSearched = false;
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
            comboboxHoverColor = themeService.IsLightTheme ? (Color)ColorConverter.ConvertFromString("#C5C7C9") : (Color)ColorConverter.ConvertFromString("#202326");
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

        private Color _comboboxHoverColor;
        public Color comboboxHoverColor
        {
            get => _comboboxHoverColor;
            set => SetAndNotify(ref _comboboxHoverColor, value);

        }

        private bool _Theme;
        public bool Theme
        {
            get => _Theme;
            set => SetAndNotify(ref _Theme, value);
            
        }

        private bool _IsSearched;
        public bool IsSearched
        {
            get => _IsSearched;
            set => SetAndNotify(ref _IsSearched, value);

        }
        public async void SearchCommand(string text)
        {
            Trace.WriteLine("Searching ...");
            IsSearched = false;
            await Task.Delay(TimeSpan.FromSeconds(1)); // this will be changed to the search process
            IsSearched = true;
        }
    }
}
