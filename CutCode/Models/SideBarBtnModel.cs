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
        public static List<string> SideBarBtnsDarkTheme = new List<string>() { "home_white.png", "add_white.png", "fav_white.png", "settings_white.png" };
        public static List<string> SideBarBtnsLightTheme = new List<string>() { "home_black.png", "add_black.png", "fav_black.png", "settings_black.png" };

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
            background = ColorCon.Convert("#00FFFFFF");

            SetAppearance();
        }
        private void ThemeChanged(object sender, EventArgs e)
        {
            SetAppearance();
        }

        private void SetAppearance()
        {
            imageSource = themeService.IsLightTheme ? $"../Resources/Images/Icons/{btnBothimages[0]}" : $"../Resources/Images/Icons/{btnBothimages[1]}";

            toolTipBackground = themeService.IsLightTheme ? ColorCon.Convert("#CBD0D5") : ColorCon.Convert("#1E1E1E");
            toolTipForeground = themeService.IsLightTheme ? ColorCon.Convert("#060607") : ColorCon.Convert("#94969A");

            
            if (background.Color != ColorCon.Convert("#00FFFFFF").Color)
            {
                background = themeService.IsLightTheme ? ColorCon.Convert("#FCFCFC") : ColorCon.Convert("#36393F");
            }
        }
        public string toolTipText { get; set; }

        private SolidColorBrush _background;
        public SolidColorBrush background
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

        private SolidColorBrush _toolTipBackground;
        public SolidColorBrush toolTipBackground
        {
            get => _toolTipBackground;
            set { SetAndNotify(ref _toolTipBackground, value); }
        }

        private SolidColorBrush _toolTipForeground;
        public SolidColorBrush toolTipForeground
        {
            get => _toolTipForeground;
            set { SetAndNotify(ref _toolTipForeground, value); }
        }
    }
}
