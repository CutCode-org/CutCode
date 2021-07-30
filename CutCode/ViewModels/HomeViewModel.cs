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
            searchBarBackground = themeService.IsLightTheme ? ColorCon.Convert("#DADBDC") : ColorCon.Convert("#2A2E33");
            searchBarTextColor = themeService.IsLightTheme ? ColorCon.Convert("#000000") : ColorCon.Convert("#FFFFFF");
            searchBarHoverColor = themeService.IsLightTheme ? ColorCon.Convert("#D0D1D2") : ColorCon.Convert("#373737");
            comboboxHoverColor = themeService.IsLightTheme ? ColorCon.Convert("#C5C7C9") : ColorCon.Convert("#202326");
        }

        private SolidColorBrush _searchBarBackground;
        public SolidColorBrush searchBarBackground
        {
            get => _searchBarBackground;
            set =>SetAndNotify(ref _searchBarBackground, value);
            
        }

        private SolidColorBrush _searchBarHoverColor;
        public SolidColorBrush searchBarHoverColor
        {
            get => _searchBarHoverColor;
            set => SetAndNotify(ref _searchBarHoverColor, value);

        }

        private SolidColorBrush _searchBarTextColor;
        public SolidColorBrush searchBarTextColor
        {
            get => _searchBarTextColor;
            set =>  SetAndNotify(ref _searchBarTextColor, value);
            
        }

        private SolidColorBrush _comboboxHoverColor;
        public SolidColorBrush comboboxHoverColor
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
