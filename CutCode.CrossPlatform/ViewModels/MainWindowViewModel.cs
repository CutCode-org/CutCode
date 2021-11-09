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
        public ObservableCollection<SideBarBtnModel> sideBarBtns { get; set; }

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
            
            pageService.PageChanged += PageChanged;
            pageService.PageRemoteChanged += PageRemoteChanged;
            
            sideBarBtns = new ObservableCollection<SideBarBtnModel>();

            sideBarBtns.Add(new SideBarBtnModel("Home", _themeService));
            sideBarBtns.Add(new SideBarBtnModel("Add", _themeService));
            sideBarBtns.Add(new SideBarBtnModel("Favourite", _themeService));
            sideBarBtns.Add(new SideBarBtnModel("Settings", _themeService));
            
            sideBarBtns[0].background = _themeService.IsLightTheme ? Color.Parse("#FCFCFC") : Color.Parse("#36393F");
        }
        
        private void ThemeChanged(object sender, EventArgs e)
        {
            backgroundColor = themeService.IsLightTheme ? Color.Parse("#FCFCFC") : Color.Parse("#36393F");
            titleBarColor = themeService.IsLightTheme ? Color.Parse("#E3E5E8") : Color.Parse("#202225");
            SideBarColor = themeService.IsLightTheme ? Color.Parse("#F2F3F5") : Color.Parse("#2A2E33");
            mainTextColor = themeService.IsLightTheme ? Color.Parse("#0B0B13") : Color.Parse("#94969A");

            exitImage = themeService.IsLightTheme ? ImageFromUri($"avares://CutCode.CrossPlatform/Assets/Images/Icons/exit_black.png") : ImageFromUri($"avares://CutCode.CrossPlatform/Assets/Images/Icons/exit_white.png");
            minImage = themeService.IsLightTheme ? ImageFromUri($"avares://CutCode.CrossPlatform/Assets/Images/Icons/min_black.png") : ImageFromUri($"avares://CutCode.CrossPlatform/Assets/Images/Icons/min_white.png");
            maxImage = themeService.IsLightTheme ? ImageFromUri($"avares://CutCode.CrossPlatform/Assets/Images/Icons/max_black.png") : ImageFromUri($"avares://CutCode.CrossPlatform/Assets/Images/Icons/max_white.png");

            titlebarBtnsHoverColor = themeService.IsLightTheme ? Color.Parse("#D0D1D2") : Color.Parse("#373737");
        }

        private IImage ImageFromUri(string path)
        {
            var uri = new Uri(path);
            return assetLoader.Exists(uri) ? new Bitmap(assetLoader.Open(uri)) : throw new Exception("WTF");
        } 
        
        private object _currentPage;
        public object currentPage
        {
            get => _currentPage;
            set => this.RaiseAndSetIfChanged(ref _currentPage, value);
        }

        private void PageChanged(object sender, EventArgs e) => currentPage = pageService.Page;

        private void PageRemoteChanged(object sender, EventArgs e)
        {
            var page = sender as string;
            ChangePageCommand(page);
        }

        public void ChangePageCommand(string selected_item)
        {
            int ind = 0;
            foreach (var btn in sideBarBtns)
            {
                if (btn.toolTipText != selected_item) btn.background = Color.Parse("#00FFFFFF");
                else ind = sideBarBtns.IndexOf(btn);
            }

            sideBarBtns[ind].background = themeService.IsLightTheme ? Color.Parse("#FCFCFC") : Color.Parse("#36393F");
            if (currentPage != Pages[ind]) 
            {
                pageService.Page = Pages[ind];
            }
        }
        
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

        private IImage _exitImage;
        public IImage exitImage
        {
            get => _exitImage;
            set => this.RaiseAndSetIfChanged(ref _exitImage, value);
        }

        private IImage _maxImage;
        public IImage maxImage
        {
            get => _maxImage;
            set => this.RaiseAndSetIfChanged(ref _maxImage, value);
        }

        private IImage _minImage;
        public IImage minImage
        {
            get => _minImage;
            set => this.RaiseAndSetIfChanged(ref _minImage, value);
        }
    }
}
