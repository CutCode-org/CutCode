using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CutCode
{
    public class NotificationDialogViewModel : Screen
    {
        private readonly IThemeService themeService;
        public NotificationDialogViewModel(IThemeService _themeService, string _message)
        {
            message = _message;
            themeService = _themeService;
            SetAppearance(null, null);
            themeService.ThemeChanged += SetAppearance;
        }

        private void SetAppearance(object sender, EventArgs e)
        {
            background = themeService.IsLightTheme ? ColorCon.Convert("#DCDCDC") : ColorCon.Convert("#26292F");
            textColor = themeService.IsLightTheme ? ColorCon.Convert("#060607") : ColorCon.Convert("#F0F0F0");
        }

        private string _message;
        public string message
        {
            get => _message;
            set
            { SetAndNotify(ref _message, value); }
        }

        private SolidColorBrush _background;
        public SolidColorBrush background
        {
            get => _background;
            set
            { SetAndNotify(ref _background, value); }
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
    }
}
