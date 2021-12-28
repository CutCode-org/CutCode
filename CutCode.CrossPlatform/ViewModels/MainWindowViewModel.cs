using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using ReactiveUI;
using Avalonia.Media.Imaging;
using CutCode.CrossPlatform.Helpers;
using CutCode.CrossPlatform.Interfaces;
using CutCode.CrossPlatform.Models;
using CutCode.CrossPlatform.Views;
using CutCode.DataBase;

namespace CutCode.CrossPlatform.ViewModels
{
    public class MainWindowViewModel : PageBaseViewModel
    {
        public ObservableCollection<TabItemModel> Tabs { get; set;  }
        protected override void OnLoad()
        {
            Tabs = new ObservableCollection<TabItemModel>();
            Tabs.Add(new TabItemModel()
            {
                ToolTip = "Home",
                Path = IconPaths.Home,
                Content = new HomeView()
            });
            Tabs.Add(new TabItemModel()
            {
                ToolTip = "Add",
                Path = IconPaths.Add,
                Content = new AddView()
            });
            Tabs.Add(new TabItemModel()
            {
                ToolTip = "Favourite",
                Path = IconPaths.Favourite,
                Content = new FavoritesView()
            });
            Tabs.Add(new TabItemModel()
            {
                ToolTip = "Settings",
                Path = IconPaths.Setting,
                Content = new SettingsView()
            });

            CurrentTabItem = 0;
            
            PageService.Current.TabChanged += (sender, args) =>
            {
                CurrentTabItem = PageService.Current.CurrentTabIndex;
            };

            PageService.Current.ExternalPageChange += (sender, args) =>
            {
                Tabs[0].Content = PageService.Current.ExternalPage;
                CurrentTabItem = 0;
            };
        }

        protected override void OnLightThemeIsSet()
        {
            windowsBtnColor = Color.Parse("#090909");
            backgroundColor = Color.Parse("#FCFCFC");
            titleBarColor = Color.Parse("#E3E5E8");
            SideBarColor = Color.Parse("#F2F3F5");
            mainTextColor = Color.Parse("#0B0B13");

            titlebarBtnsHoverColor = Color.Parse("#D0D1D2");
        }

        protected override void OnDarkThemeIsSet()
        {
            windowsBtnColor = Color.Parse("#F2F2F2");
            backgroundColor = Color.Parse("#36393F");
            titleBarColor = Color.Parse("#202225");
            SideBarColor = Color.Parse("#32363C");
            mainTextColor = Color.Parse("#94969A");

            titlebarBtnsHoverColor = Color.Parse("#373737");
        }
        
        private int _currentTabItem;
        public int CurrentTabItem
        {
            get => _currentTabItem;
            set
            {
                this.RaiseAndSetIfChanged(ref _currentTabItem, value);
                if (_currentTabItem != 0 && Tabs[0].Content is not HomeView)
                {
                    Tabs[0].Content = new HomeView();
                }
            }
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
