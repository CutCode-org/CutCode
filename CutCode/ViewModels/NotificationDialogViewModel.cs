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
            exitBtnHoverColor = themeService.IsLightTheme ? ColorCon.Convert("#D0D1D2") : ColorCon.Convert("#1F232B");
            textColor = themeService.IsLightTheme ? ColorCon.Convert("#060607") : ColorCon.Convert("#94969A");
            exitImage = themeService.IsLightTheme ? "../Resources/Images/Icons/exit_black.png" : "../Resources/Images/Icons/exit_white.png";
        }

        private string _exitImage;
        public string exitImage
        {
            get => _exitImage;
            set
            { SetAndNotify(ref _exitImage, value); }
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

        private SolidColorBrush _exitBtnHoverColor;
        public SolidColorBrush exitBtnHoverColor
        {
            get => _exitBtnHoverColor;
            set
            {
                if (value != _exitBtnHoverColor)
                {
                    SetAndNotify(ref _exitBtnHoverColor, value);
                }
            }

        }

        public void ExitCommand()
        {

        }
    }
}
