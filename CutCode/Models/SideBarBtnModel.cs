using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CutCode
{
    public static class SideBarBtns
    {
        public static List<string> AllSideBarBtns = new List<string>() { "Home", "Add", "Favourite", "Settings"};
        public static List<string> SideBarBtnsDarkTheme = new List<string>() { "home_white.png", "add_white.png", "favourite_white.png", "settings_white.png" };
        public static List<string> SideBarBtnsLightTheme = new List<string>() { "home_black.png", "add_black.png", "favourite_black.png", "settings_black.png" };

    }
    public class SideBarBtnModel : Screen
    {
        private readonly IThemeService themeService;
        private List<string> btnBothimages = new List<string>();
        public SideBarBtnModel(string _toolTipText, IThemeService _themeService)
        {
            themeService = _themeService;
            themeService.ThemeChanged += ThemeChanged;

            toolTipText = _toolTipText;
            var index = SideBarBtns.AllSideBarBtns.IndexOf(_toolTipText);
            btnBothimages.Add(SideBarBtns.SideBarBtnsLightTheme[index]);
            btnBothimages.Add(SideBarBtns.SideBarBtnsDarkTheme[index]);
            background = (Color)ColorConverter.ConvertFromString("#00FFFFFF");

            SetAppearance();
        }
        private void ThemeChanged(object sender, EventArgs e)
        {
            SetAppearance();
        }

        private void SetAppearance()
        {
            imageSource = themeService.IsLightTheme ? $"../Resources/Images/Icons/{btnBothimages[0]}" : $"../Resources/Images/Icons/{btnBothimages[1]}";

            buttonHoverColor = themeService.IsLightTheme ? (Color)ColorConverter.ConvertFromString("#D6D7DA") : (Color)ColorConverter.ConvertFromString("#46494E");

            toolTipBackground = themeService.IsLightTheme ? (Color)ColorConverter.ConvertFromString("#CBD0D5") : (Color)ColorConverter.ConvertFromString("#1E1E1E");
            toolTipForeground = themeService.IsLightTheme ? (Color)ColorConverter.ConvertFromString("#060607") : (Color)ColorConverter.ConvertFromString("#94969A");

            if (background != (Color)ColorConverter.ConvertFromString("#00FFFFFF"))
            {
                background = themeService.IsLightTheme ? (Color)ColorConverter.ConvertFromString("#FCFCFC") : (Color)ColorConverter.ConvertFromString("#36393F");
            }
        }
        public string toolTipText { get; set; }

        private Color _background;
        public Color background
        {
            get => _background;
            set
            {
                if(value != _background)
                {
                    SetAndNotify(ref _background, value);
                }
            }
        }

        private string _imageSource;
        public string imageSource
        {
            get => _imageSource;
            set
            {
                if (value != this._imageSource)
                {
                    SetAndNotify(ref _imageSource, value);
                }
            }
        }

        private Color _toolTipBackground;
        public Color toolTipBackground
        {
            get => _toolTipBackground;
            set { SetAndNotify(ref _toolTipBackground, value); }
        }

        private Color _toolTipForeground;
        public Color toolTipForeground
        {
            get => _toolTipForeground;
            set { SetAndNotify(ref _toolTipForeground, value); }
        }

        
        private Color _buttonHoverColor;
        public Color buttonHoverColor
        {
            get => _buttonHoverColor;
            set { SetAndNotify(ref _buttonHoverColor, value); }
        }
    }
}
