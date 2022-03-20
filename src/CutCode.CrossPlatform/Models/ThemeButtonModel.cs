using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;
using CutCode.CrossPlatform.Interfaces;
using CutCode.CrossPlatform.Services;
using CutCode.CrossPlatform.ViewModels;
using ReactiveUI;

namespace CutCode.CrossPlatform.Models
{
    public class ThemeButtonModel : ViewModelBase
    {
        private readonly ThemeService themeService;
        public ThemeButtonModel(string _text, ThemeService _themeService)
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
            textColor = themeService.Theme == ThemeType.Light ? SolidColorBrush.Parse("#0B0B13") : SolidColorBrush.Parse("#94969A");
            if (themeService.Theme == ThemeType.Light)
            {
                if (text == "Light Mode")
                {
                    backgroundColor = SolidColorBrush.Parse("#DADDE1");
                    ToggleImage = $"../Resources/Images/Icons/toggle_on_black.png";
                    HoverbackgroundColor = SolidColorBrush.Parse("#00FFFFFF");
                }

                else
                {
                    backgroundColor = SolidColorBrush.Parse("#00FFFFFF");
                    ToggleImage = $"../Resources/Images/Icons/toggle_off_black.png";
                    HoverbackgroundColor = SolidColorBrush.Parse("#E5E6E8");
                }

            }
            else
            {
                if (text == "Dark Mode")
                {
                    backgroundColor = SolidColorBrush.Parse("#202225");
                    ToggleImage = $"../Resources/Images/Icons/toggle_on_white.png";
                    HoverbackgroundColor = SolidColorBrush.Parse("#00FFFFFF");
                }
                else
                {
                    backgroundColor = SolidColorBrush.Parse("#00FFFFFF");
                    ToggleImage = $"../Resources/Images/Icons/toggle_off_white.png";
                    HoverbackgroundColor = SolidColorBrush.Parse("#27282C");
                }
            }
        }

        private string _text;
        public string text
        {
            get => _text;
            set { this.RaiseAndSetIfChanged(ref _text, value); }
        }

        private SolidColorBrush _textColor;
        public SolidColorBrush textColor
        {
            get => _textColor;
            set
            {
                if (value != _textColor)
                {
                    this.RaiseAndSetIfChanged(ref _textColor, value);
                }
            }
        }

        private string _ToggleImage;
        public string ToggleImage
        {
            get => _ToggleImage;
            set { this.RaiseAndSetIfChanged(ref _ToggleImage, value); }
        }

        private SolidColorBrush _backgroundColor;
        public SolidColorBrush backgroundColor
        {
            get => _backgroundColor;
            set
            {
                if (value != _backgroundColor)
                {
                    this.RaiseAndSetIfChanged(ref _backgroundColor, value);
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
                    this.RaiseAndSetIfChanged(ref _HoverbackgroundColor, value);
                }
            }
        }
    }
}
