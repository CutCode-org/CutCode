using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        
        public static List<System.Object> Pages;
        public ObservableCollection<SideBarBtnModel> sideBarBtns { get; set; }

        public MainWindowViewModel()
        {
            // for the xaml load
        }
        public MainWindowViewModel(ThemeService _themeService, IPageService _pageService, IDataBase database)
        {
            themeService = _themeService;
            themeService.ThemeChanged += ThemeChanged;
            themeService.IsLightTheme = database.isLightTheme;
            
            pageService = _pageService;
            
            pageService.PageChanged += PageChanged;
            pageService.PageRemoteChanged += PageRemoteChanged;
        }
        
        private void ThemeChanged(object sender, EventArgs e)
        {
            backgroundColor = themeService.IsLightTheme ? SolidColorBrush.Parse("#FCFCFC") : SolidColorBrush.Parse("#36393F");
            titleBarColor = themeService.IsLightTheme ? SolidColorBrush.Parse("#E3E5E8") : SolidColorBrush.Parse("#202225");
            SideBarColor = themeService.IsLightTheme ? SolidColorBrush.Parse("#F2F3F5") : SolidColorBrush.Parse("#2A2E33");
            mainTextColor = themeService.IsLightTheme ? SolidColorBrush.Parse("#0B0B13") : SolidColorBrush.Parse("#94969A");

            exitImage = themeService.IsLightTheme ? "../Resources/Images/Icons/exit_black.png" : "../Resources/Images/Icons/exit_white.png";
            minImage = themeService.IsLightTheme ? "../Resources/Images/Icons/min_black.png" : "../Resources/Images/Icons/min_white.png";
            maxImage = themeService.IsLightTheme ? "../Resources/Images/Icons/max_black.png" : "../Resources/Images/Icons/max_white.png";

            titlebarBtnsHoverColor = themeService.IsLightTheme ? SolidColorBrush.Parse("#D0D1D2") : SolidColorBrush.Parse("#373737");
        }

        private System.Object _currentPage;
        public System.Object currentPage
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
                if (btn.toolTipText != selected_item) btn.background = SolidColorBrush.Parse("#00FFFFFF");
                else ind = sideBarBtns.IndexOf(btn);
            }

            sideBarBtns[ind].background = themeService.IsLightTheme ? SolidColorBrush.Parse("#FCFCFC") : SolidColorBrush.Parse("#36393F");
            if (currentPage != Pages[ind]) 
            {
                pageService.Page = Pages[ind];
            }
        }
        
        #region Color
        private SolidColorBrush _backgroundColor;
        public SolidColorBrush backgroundColor
        {
            get => _backgroundColor;
            set => this.RaiseAndSetIfChanged(ref _backgroundColor, value);
        }

        private SolidColorBrush _titleBarColor;
        public SolidColorBrush titleBarColor
        {
            get => _titleBarColor;
            set => this.RaiseAndSetIfChanged(ref _titleBarColor, value);
        }

        private SolidColorBrush _sideBarColor;
        public SolidColorBrush SideBarColor
        {
            get => _sideBarColor;
            set => this.RaiseAndSetIfChanged(ref _sideBarColor, value);
        }

        private SolidColorBrush _mainTextColor;
        public SolidColorBrush mainTextColor
        {
            get => _mainTextColor;
            set => this.RaiseAndSetIfChanged(ref _mainTextColor, value);
        }

        private SolidColorBrush _titlebarBtnsHoverColor;
        public SolidColorBrush titlebarBtnsHoverColor
        {
            get => _titlebarBtnsHoverColor;
            set => this.RaiseAndSetIfChanged(ref _titlebarBtnsHoverColor, value);
        }
        #endregion

        private string _exitImage;
        public string exitImage
        {
            get => _exitImage;
            set => this.RaiseAndSetIfChanged(ref _exitImage, value);
        }

        private string _maxImage;
        public string maxImage
        {
            get => _maxImage;
            set => this.RaiseAndSetIfChanged(ref _maxImage, value);
        }

        private string _minImage;
        public string minImage
        {
            get => _minImage;
            set => this.RaiseAndSetIfChanged(ref _minImage, value);
        }
    }
}
