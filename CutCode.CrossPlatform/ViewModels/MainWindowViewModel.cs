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

namespace CutCode.CrossPlatform.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ThemeService themeService;
        private readonly IPageService pageService;
        private readonly IAssetLoader assetLoader;
        
        public static List<System.Object> Pages;

        public MainWindowViewModel()
        {
            // for the xaml load
            assetLoader = AvaloniaLocator.CurrentMutable.GetService<IAssetLoader>();
        }
        public MainWindowViewModel(ThemeService _themeService, IPageService _pageService, IDataBase database)
        {
            assetLoader = AvaloniaLocator.CurrentMutable.GetService<IAssetLoader>();
            themeService = _themeService;
            themeService.ThemeChanged += ThemeChanged;
            themeService.IsLightTheme = database.isLightTheme;
            
            pageService = _pageService;
            HomeViewModel = new HomeViewModel(themeService, pageService, database);

            //sideBarBtns[0].background = _themeService.IsLightTheme ? Color.Parse("#FCFCFC") : Color.Parse("#36393F");
        }
        
        private void ThemeChanged(object sender, EventArgs e)
        {
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

        #region Color
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
