using Stylet;
using StyletIoC;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace CutCode
{
    public class MainViewModel : Screen
    {
        //private List<Button> leftBarBtns;
        public static List<Object> Pages;
        private readonly IThemeService _themeService;
        private IWindowManager windowManager;
        private readonly IPageService pageService;
        private readonly INotificationManager notifyManager;
        public ObservableCollection<SideBarBtnModel> sideBarBtns { get; set; }
        public MainViewModel(IWindowManager _windowManager,IThemeService themeService, IPageService _pageService, IDataBase _dataBase, INotificationManager _notifyManager)
        {
            windowManager = _windowManager;

            notifyManager = _notifyManager;
            notifyManager.ShowNotification += showNotification;

            _themeService = themeService;
            _themeService.ThemeChanged += ThemeChanged;
            _themeService.IsLightTheme = _dataBase.isLightTheme;

            pageService = _pageService;
            pageService.PageChanged += PageChanged;
            pageService.PageRemoteChanged += PageRemoteChanged;

            sideBarBtns = new ObservableCollection<SideBarBtnModel>();

            // there should be some kind of condition here
            sideBarBtns.Add(new SideBarBtnModel("Home", _themeService));
            sideBarBtns.Add(new SideBarBtnModel("Add", _themeService));
            sideBarBtns.Add(new SideBarBtnModel("Favourite", _themeService));
            sideBarBtns.Add(new SideBarBtnModel("Share", _themeService));
            sideBarBtns.Add(new SideBarBtnModel("Settings", _themeService));
            sideBarBtns[0].background = _themeService.IsLightTheme ? ColorCon.Convert("#FCFCFC") : ColorCon.Convert("#36393F");


            Pages = new List<Object>() {    new HomeViewModel(_themeService, pageService, _dataBase), 
                                            new AddViewModel(_themeService, pageService, _dataBase), 
                                            new FavViewModel(_themeService, pageService, _dataBase),
                                            new ShareViewModel(_themeService, pageService, _dataBase),
                                            new SettingViewModel(_themeService, _dataBase) };
            pageService.Page = Pages[0];
        }

        private void ThemeChanged(object sender, EventArgs e)
        {
            backgroundColor = _themeService.IsLightTheme ? ColorCon.Convert("#FCFCFC") : ColorCon.Convert("#36393F");
            titleBarColor = _themeService.IsLightTheme ? ColorCon.Convert("#E3E5E8") : ColorCon.Convert("#202225");
            SideBarColor = _themeService.IsLightTheme ? ColorCon.Convert("#F2F3F5") : ColorCon.Convert("#2A2E33");
            mainTextColor = _themeService.IsLightTheme ? ColorCon.Convert("#0B0B13") : ColorCon.Convert("#94969A");

            exitImage = _themeService.IsLightTheme ? "../Resources/Images/Icons/exit_black.png" : "../Resources/Images/Icons/exit_white.png";
            minImage = _themeService.IsLightTheme ? "../Resources/Images/Icons/min_black.png" : "../Resources/Images/Icons/min_white.png";
            maxImage = _themeService.IsLightTheme ? "../Resources/Images/Icons/max_black.png" : "../Resources/Images/Icons/max_white.png";

            titlebarBtnsHoverColor = _themeService.IsLightTheme ? ColorCon.Convert("#D0D1D2") : ColorCon.Convert("#373737");
        }

        private Object _currentPage;
        public Object currentPage
        {
            get => _currentPage;
            set { SetAndNotify(ref _currentPage, value); }
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
                if (btn.toolTipText != selected_item) btn.background = ColorCon.Convert("#00FFFFFF");
                else ind = sideBarBtns.IndexOf(btn);
            }

            sideBarBtns[ind].background = _themeService.IsLightTheme ? ColorCon.Convert("#FCFCFC") : ColorCon.Convert("#36393F");
            if (currentPage != Pages[ind]) pageService.Page = Pages[ind];

            if(selected_item == "Share")
            {
                notifyManager.CreateNotification("This is just for test", 6);
            }
        }

        #region Color
        private SolidColorBrush _backgroundColor;
        public SolidColorBrush backgroundColor
        {
            get => _backgroundColor;
            set
            {
                if (value != _backgroundColor)
                {
                    SetAndNotify(ref _backgroundColor, value);
                }
            }
        }

        private SolidColorBrush _titleBarColor;
        public SolidColorBrush titleBarColor
        {
            get => _titleBarColor;
            set
            {
                if (value != _titleBarColor)
                {
                    SetAndNotify(ref _titleBarColor, value);
                }
            }
        }

        private SolidColorBrush _sideBarColor;
        public SolidColorBrush SideBarColor
        {
            get => _sideBarColor;
            set
            {
                if (value != _sideBarColor)
                {
                    SetAndNotify(ref _sideBarColor, value);
                }
            }
        }

        private SolidColorBrush _mainTextColor;
        public SolidColorBrush mainTextColor
        {
            get => _mainTextColor;
            set
            {
                if (value != _mainTextColor)
                {
                    SetAndNotify(ref _mainTextColor, value);
                }
            }
        }

        private SolidColorBrush _titlebarBtnsHoverColor;
        public SolidColorBrush titlebarBtnsHoverColor
        {
            get => _titlebarBtnsHoverColor;
            set
            {
                if (value != _titlebarBtnsHoverColor)
                {
                    SetAndNotify(ref _titlebarBtnsHoverColor, value);
                }
            }
        }
        #endregion

        private string _exitImage;
        public string exitImage
        {
            get => _exitImage;
            set
            { SetAndNotify(ref _exitImage, value); }
        }

        private string _maxImage;
        public string maxImage
        {
            get => _maxImage;
            set
            { SetAndNotify(ref _maxImage, value); }
        }

        private string _minImage;
        public string minImage
        {
            get => _minImage;
            set
            { SetAndNotify(ref _minImage, value); }
        }

        #region NotificationDialogView
        private Object _notificationDialogView;
        public Object notificationDialogView
        {
            get => _notificationDialogView;
            set => SetAndNotify(ref _notificationDialogView, value);
        }

        private Visibility _notifcationDialogVisibility;
        public Visibility notifcationDialogVisibility
        {
            get => _notifcationDialogVisibility;
            set => SetAndNotify(ref _notifcationDialogVisibility, value);
        }

        private void showNotification(object sender, EventArgs e)
        {
            var noitification = sender as NotifyModel;

            var notifcationViewModel = new NotificationDialogViewModel(_themeService, noitification.Message);
            notificationDialogView = (Object)notifcationViewModel;

            notifcationDialogVisibility = Visibility.Visible;
            var closeTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(noitification.Delay),
                IsEnabled = true
            };
            closeTimer.Tick += (object sender, EventArgs e) =>
            {
                notifcationDialogVisibility = Visibility.Hidden;
                closeTimer.Stop();
            };
        }
        #endregion
    }
}
