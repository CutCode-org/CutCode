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
        private readonly IPageService pageService;

        public MainWindowViewModel()
        {
            
        }
        public MainWindowViewModel(ThemeService _themeService, IPageService _pageService, IDataBase database)
        {
            themeService = _themeService;
            themeService.ThemeChanged += ThemeChanged;
        }

        private void ThemeChanged(object sender, EventArgs e)
        {
            UpdateTheme();
        }

        private void UpdateTheme()
        {
        }

        private Brush _backgroundColor;

        public Brush BackgroundColor
        {
            get => _backgroundColor;
            set => this.RaiseAndSetIfChanged(ref _backgroundColor, value);
        }
    }
}
