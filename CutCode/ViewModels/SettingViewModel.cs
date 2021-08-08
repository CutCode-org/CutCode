using Stylet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CutCode
{
    public class SettingViewModel : Screen
    {
        public ObservableCollection<ThemeButtonModel> themeBtns { get; set; }
        private readonly IThemeService themeService;
        private readonly IDataBase database;
        public SettingViewModel(IThemeService _themeService, IDataBase _database)
        {
            database = _database;

            themeService = _themeService;
            themeService.ThemeChanged += ThemeChanged;

            themeBtns = new ObservableCollection<ThemeButtonModel>();
            themeBtns.Add(new ThemeButtonModel("Light Mode", themeService));
            themeBtns.Add(new ThemeButtonModel("Dark Mode", themeService));

            mainTextColor = themeService.IsLightTheme ? ColorCon.Convert("#0B0B13") : ColorCon.Convert("#94969A");
            cardBackgroundColor = themeService.IsLightTheme ? ColorCon.Convert("#F2F3F5") : ColorCon.Convert("#2F3136");
        }

        private void ThemeChanged(object sender, EventArgs e)
        {
            mainTextColor = themeService.IsLightTheme ? ColorCon.Convert("#0B0B13") : ColorCon.Convert("#94969A");
            cardBackgroundColor = themeService.IsLightTheme ? ColorCon.Convert("#F2F3F5") : ColorCon.Convert("#2F3136");
        }

        private SolidColorBrush _mainTextColor;
        public SolidColorBrush mainTextColor
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

        private SolidColorBrush _cardBackgroundColor;
        public SolidColorBrush cardBackgroundColor
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
        public void ThemeChangeCommand(string selectedTheme) 
        {
            themeService.IsLightTheme = selectedTheme == "Light Mode" ? true : false;
            database.ChangeTheme(themeService.IsLightTheme);
        } 

    }
}
