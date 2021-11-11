using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Avalonia;
using Avalonia.Controls.Templates;
using Avalonia.Media;
using Avalonia.Platform;
using CutCode.CrossPlatform.Interfaces;
using CutCode.CrossPlatform.Models;
using ReactiveUI;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using CutCode.DataBase;

namespace CutCode.CrossPlatform.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ThemeService themeService;
        private readonly IAssetLoader assetLoader;
        public MainWindowViewModel()
        {
            assetLoader = AvaloniaLocator.CurrentMutable.GetService<IAssetLoader>();
            
            themeService = AvaloniaLocator.CurrentMutable.GetService<ThemeService>();
            themeService.ThemeChanged += ThemeChanged;
            
            var database = AvaloniaLocator.CurrentMutable.GetService<DataBaseManager>();
            themeService.IsLightTheme = database.isLightTheme;
        }
        
        private void ThemeChanged(object sender, EventArgs e)
        {
            windowsBtnColor = themeService.IsLightTheme ? Color.Parse("#090909") : Color.Parse("#F2F2F2");
            backgroundColor = themeService.IsLightTheme ? Color.Parse("#FCFCFC") : Color.Parse("#36393F");
            titleBarColor = themeService.IsLightTheme ? Color.Parse("#E3E5E8") : Color.Parse("#202225");
            SideBarColor = themeService.IsLightTheme ? Color.Parse("#F2F3F5") : Color.Parse("#2A2E33");
            mainTextColor = themeService.IsLightTheme ? Color.Parse("#0B0B13") : Color.Parse("#94969A");

            titlebarBtnsHoverColor = themeService.IsLightTheme ? Color.Parse("#D0D1D2") : Color.Parse("#373737");
        }

        private IImage ImageFromUri(string path)
        {
            var uri = new Uri(path);
            return assetLoader.Exists(uri) ? new Bitmap(assetLoader.Open(uri)) : throw new Exception("WTF");
        }

        #region Pages

        private HomeViewModel _HomeViewModel;

        public HomeViewModel HomeViewModel
        {
            get => _HomeViewModel;
            set => this.RaiseAndSetIfChanged(ref _HomeViewModel, value);
        }
        #endregion

        public ThemeService ThemeService
        {
            get => themeService;
            set => this.RaiseAndSetIfChanged(ref themeService, value);
        }
        
        #region Color
        private Color _windowsBtnColor;
        public Color windowsBtnColor
        {
            get => _windowsBtnColor;
            set => this.RaiseAndSetIfChanged(ref _windowsBtnColor, value);
        }
        
        private Color _backgroundColor;
        public Color backgroundColor
        {
            get => _backgroundColor;
            set => this.RaiseAndSetIfChanged(ref _backgroundColor, value);
        }

        private Color _titleBarColor;
        public Color titleBarColor
        {
            get => _titleBarColor;
            set => this.RaiseAndSetIfChanged(ref _titleBarColor, value);
        }

        private Color _sideBarColor;
        public Color SideBarColor
        {
            get => _sideBarColor;
            set => this.RaiseAndSetIfChanged(ref _sideBarColor, value);
        }

        private Color _mainTextColor;
        public Color mainTextColor
        {
            get => _mainTextColor;
            set => this.RaiseAndSetIfChanged(ref _mainTextColor, value);
        }

        private Color _titlebarBtnsHoverColor;
        public Color titlebarBtnsHoverColor
        {
            get => _titlebarBtnsHoverColor;
            set => this.RaiseAndSetIfChanged(ref _titlebarBtnsHoverColor, value);
        }
        #endregion

    }
}
