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
            backgroundColor = themeService.IsLightTheme ? Color.Parse("#FCFCFC") : Color.Parse("#36393F");
            titleBarColor = themeService.IsLightTheme ? Color.Parse("#E3E5E8") : Color.Parse("#202225");
            SideBarColor = themeService.IsLightTheme ? Color.Parse("#F2F3F5") : Color.Parse("#2A2E33");
            mainTextColor = themeService.IsLightTheme ? Color.Parse("#0B0B13") : Color.Parse("#94969A");

            exitImage = themeService.IsLightTheme ? "../Resources/Images/Icons/exit_black.png" : "../Resources/Images/Icons/exit_white.png";
            minImage = themeService.IsLightTheme ? "../Resources/Images/Icons/min_black.png" : "../Resources/Images/Icons/min_white.png";
            maxImage = themeService.IsLightTheme ? "../Resources/Images/Icons/max_black.png" : "../Resources/Images/Icons/max_white.png";

            titlebarBtnsHoverColor = themeService.IsLightTheme ? Color.Parse("#D0D1D2") : Color.Parse("#373737");
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
