using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CutCode
{
    public class ThemeButtonModel : Screen
    {
        private readonly IThemeService themeService;
        public ThemeButtonModel(string _text, IThemeService _themeService)
        {
            themeService = _themeService;
            themeService.ThemeChanged += ThemeChanged;
            text = _text;
            SetAppearance();
        }

        private void ThemeChanged(object sender, EventArgs e)
        {
            SetAppearance();
        }

        private void SetAppearance()
        {
            textColor = themeService.IsLightTheme ? (Color)ColorConverter.ConvertFromString("#0B0B13") : (Color)ColorConverter.ConvertFromString("#94969A");
            if (themeService.IsLightTheme == true)
            {
                if (text == "Light Mode")
                {
                    backgroundColor = (Color)ColorConverter.ConvertFromString("#DADDE1");
                    ToggleImage = $"../Resources/Images/Icons/toggle_on_black.png";
                    HoverbackgroundColor = (Color)ColorConverter.ConvertFromString("#00FFFFFF");
                }

                else
                {
                    backgroundColor = (Color)ColorConverter.ConvertFromString("#00FFFFFF");
                    ToggleImage = $"../Resources/Images/Icons/toggle_off_black.png";
                    HoverbackgroundColor = (Color)ColorConverter.ConvertFromString("#E5E6E8");
                }

            }
            else
            {
                if (text == "Dark Mode")
                {
                    backgroundColor = (Color)ColorConverter.ConvertFromString("#202225");
                    ToggleImage = $"../Resources/Images/Icons/toggle_on_white.png";
                    HoverbackgroundColor = (Color)ColorConverter.ConvertFromString("#00FFFFFF");
                }
                else
                {
                    backgroundColor = (Color)ColorConverter.ConvertFromString("#00FFFFFF");
                    ToggleImage = $"../Resources/Images/Icons/toggle_off_white.png";
                    HoverbackgroundColor = (Color)ColorConverter.ConvertFromString("#27282C");
                }
            }
        }

        private string _text;
        public string text
        {
            get => _text;
            set { SetAndNotify(ref _text, value); }
        }

        private Color _textColor;
        public Color textColor
        {
            get => _textColor;
            set
            {
                if (value != _textColor)
                {
                    SetAndNotify(ref _textColor, value);
                }
            }
        }

        private string _ToggleImage;
        public string ToggleImage
        {
            get => _ToggleImage;
            set { SetAndNotify(ref _ToggleImage, value); }
        }

        private Color _backgroundColor;
        public Color backgroundColor
        {
            get => _backgroundColor;
            set
            {
                if (value != _backgroundColor)
                {
                    SetAndNotify(ref _backgroundColor, value);
                }
            }
        }

        private Color _HoverbackgroundColor;
        public Color HoverbackgroundColor
        {
            get => _HoverbackgroundColor;
            set
            {
                if (value != _HoverbackgroundColor)
                {
                    SetAndNotify(ref _HoverbackgroundColor, value);
                }
            }
        }
    }
}
