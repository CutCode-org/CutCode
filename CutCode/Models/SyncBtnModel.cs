using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CutCode
{
    public class SyncBtnModel : PropertyChangedBase
    {
        private readonly IThemeService themeService;
        public SyncBtnModel(string _text, IThemeService _themeService)
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
            textColor = themeService.IsLightTheme ? ColorCon.Convert("#0B0B13") : ColorCon.Convert("#94969A");
            backgroundColor = themeService.IsLightTheme ? ColorCon.Convert("#E5E6E8") : ColorCon.Convert("#27282C");
            HoverbackgroundColor = themeService.IsLightTheme ? ColorCon.Convert("#DADDE1") : ColorCon.Convert("#202225");

            if (text == "Import") img = themeService.IsLightTheme ? "../Resources/Images/Icons/import_icon_black.png" : "../Resources/Images/Icons/import_icon_white.png";
            else img = themeService.IsLightTheme ? "../Resources/Images/Icons/export_icon_black.png" : "../Resources/Images/Icons/export_icon_white.png";
        }

        private string _text;
        public string text
        {
            get => _text;
            set { SetAndNotify(ref _text, value); }
        }

        private SolidColorBrush _textColor;
        public SolidColorBrush textColor
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

        private string _img;
        public string img
        {
            get => _img;
            set { SetAndNotify(ref _img, value); }
        }

        private SolidColorBrush _backgroundColor;
        public SolidColorBrush backgroundColor
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

        private SolidColorBrush _HoverbackgroundColor;
        public SolidColorBrush HoverbackgroundColor
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
