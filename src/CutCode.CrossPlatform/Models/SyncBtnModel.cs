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
    public class SyncBtnModel : ViewModelBase
    {
        private readonly ThemeService themeService;
        public SyncBtnModel(string _text, ThemeService _themeService)
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
            backgroundColor = themeService.Theme == ThemeType.Light ? SolidColorBrush.Parse("#E5E6E8") : SolidColorBrush.Parse("#27282C");
            HoverbackgroundColor = themeService.Theme == ThemeType.Light ? SolidColorBrush.Parse("#DADDE1") : SolidColorBrush.Parse("#202225");

            if (text == "Import") img = themeService.Theme == ThemeType.Light ? "../Resources/Images/Icons/import_icon_black.png" : "../Resources/Images/Icons/import_icon_white.png";
            else img = themeService.Theme == ThemeType.Light ? "../Resources/Images/Icons/export_icon_black.png" : "../Resources/Images/Icons/export_icon_white.png";
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

        private string _img;
        public string img
        {
            get => _img;
            set { this.RaiseAndSetIfChanged(ref _img, value); }
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
