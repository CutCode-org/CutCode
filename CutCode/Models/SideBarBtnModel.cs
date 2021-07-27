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
            toolTipText = _toolTipText;

            var index = SideBarBtns.AllSideBarBtns.IndexOf(_toolTipText);
            btnBothimages.Add(SideBarBtns.SideBarBtnsLightTheme[index]);
            btnBothimages.Add(SideBarBtns.SideBarBtnsDarkTheme[index]);
            imageSource = themeService.IsLightTheme ? $"../Resources/Images/Icons/{btnBothimages[0]}" : $"../Resources/Images/Icons/{btnBothimages[1]}";
            background = (Color)ColorConverter.ConvertFromString("#00FFFFFF");
            themeService.ThemeChanged += ThemeChanged;
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
        private void ThemeChanged(object sender, EventArgs e) 
        {
            imageSource = themeService.IsLightTheme ? $"../Resources/Images/Icons/{btnBothimages[0]}" : $"../Resources/Images/Icons/{btnBothimages[1]}";
            if(background != (Color)ColorConverter.ConvertFromString("#00FFFFFF"))
            {
                background = themeService.IsLightTheme ? (Color)ColorConverter.ConvertFromString("9933FF") : (Color)ColorConverter.ConvertFromString("#36393F");
            }
        } 
    }
}
