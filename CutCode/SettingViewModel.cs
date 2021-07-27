using Stylet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CutCode
{
    public partial class MainViewModel : Screen
    {
        public ObservableCollection<ThemeButtonModel> themeBtns { get; set; }
        private readonly IThemeService themeService;

        public MainViewModel(IThemeService _themeService)
        {
            themeService = _themeService;
            themeService.ThemeChanged += SettingThemeChanged;

            themeBtns = new ObservableCollection<ThemeButtonModel>();
            themeBtns.Add(new ThemeButtonModel("Light Mode", themeService));
            themeBtns.Add(new ThemeButtonModel("Dark Mode", themeService));

            mainTextColor = themeService.IsLightTheme ? (Color)ColorConverter.ConvertFromString("#0B0B13") : (Color)ColorConverter.ConvertFromString("#94969A");
            cardBackgroundColor = themeService.IsLightTheme ? (Color)ColorConverter.ConvertFromString("#E8E8E8") : (Color)ColorConverter.ConvertFromString("#25292E");
        }

        private void SettingThemeChanged(object sender, EventArgs e)
        {
            mainTextColor = themeService.IsLightTheme ? (Color)ColorConverter.ConvertFromString("#0B0B13") : (Color)ColorConverter.ConvertFromString("#94969A");
            cardBackgroundColor = themeService.IsLightTheme ? (Color)ColorConverter.ConvertFromString("#E8E8E8") : (Color)ColorConverter.ConvertFromString("#25292E");
        }

        private Color _mainTextColor;
        public Color mainTextColor
        {
            get => _mainTextColor;
            set
            {
                if (value != _mainTextColor)
                {
                    SetAndNotify(ref _mainTextColor, value);
                }
            }
        }

        private Color _cardBackgroundColor;
        public Color cardBackgroundColor
        {
            get => _cardBackgroundColor;
            set
            {
                if (value != _cardBackgroundColor)
                {
                    SetAndNotify(ref _cardBackgroundColor, value);
                }
            }
        }
        public void ThemeChangeCommand(string selectedTheme) => themeService.IsLightTheme = selectedTheme == "Light Mode" ? true : false;
    }
}
