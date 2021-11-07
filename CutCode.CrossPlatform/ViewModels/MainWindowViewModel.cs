using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls.Templates;
using Avalonia.Media;
using CutCode.CrossPlatform.Interfaces;
using CutCode.CrossPlatform.Models;
using ReactiveUI;

namespace CutCode.CrossPlatform.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ThemeService themeService;
        private ColorModel colorModel;
        public MainWindowViewModel(ThemeService _themeService)
        {
            themeService = _themeService;
            colorModel = themeService.CurrentColorModel;
            themeService.ThemeChanged += ThemeChanged;
        }

        private void ThemeChanged(object sender, EventArgs e)
        {
            var currentColorModel = (ColorModel) sender;
            colorModel = currentColorModel;
            UpdateTheme();
        }

        private void UpdateTheme()
        {
            BackgroundColor = colorModel.background;
        }

        private Brush _backgroundColor;

        public Brush BackgroundColor
        {
            get => _backgroundColor;
            set => this.RaiseAndSetIfChanged(ref _backgroundColor, value);
        }
    }
}
