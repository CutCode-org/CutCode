using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using ReactiveUI;
using Avalonia.Media.Imaging;
using Avalonia.Styling;
using Avalonia.Threading;
using CutCode.CrossPlatform.Helpers;
using CutCode.CrossPlatform.Interfaces;
using CutCode.CrossPlatform.Models;
using CutCode.CrossPlatform.Views;
using CutCode.CrossPlatform.DataBase;

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
            
            
            NotificationService.ShowNotification += ShowNotification!;
            NotificationService.OnCloseNotification += ExitNotification!;
            Notifications = new ObservableCollection<Notification>();
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
                if (value != 0 && Tabs[0].Content is not HomeView)
                {
                    Tabs[0].Content = new HomeView();
                }
                else if (_currentTabItem == 1)
                {
                    Tabs[1].Content = new AddView();
                }
                this.RaiseAndSetIfChanged(ref _currentTabItem, value);
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
        
        #region NotificationDialogView
        private ObservableCollection<Notification> _notifications;
        public ObservableCollection<Notification> Notifications
        {
            get => _notifications;
            set => this.RaiseAndSetIfChanged(ref _notifications, value);
        }

        private List<Notification> WaitingNotifications = new();
        private List<LiveNotification> liveNotifications = new();
        private void ShowNotification(object sender, EventArgs e)
        {
            var notification = sender as Notification;

            var notifcationView = new NotificationView()
            {
                DataContext = new NotificationViewModel(notification!)
            };
            notification.View = notifcationView;

            if(Notifications.Count > 2)
            {
                WaitingNotifications.Add(notification);
            }
            else
            {
                Notifications.Add(notification);

                var closeTimer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(notification.Delay),
                    IsEnabled = true
                };
                liveNotifications.Add(new LiveNotification() { Timer = closeTimer, Notification = notification});
                closeTimer.Tick += CloseNotification!;
            }
        }

        private void ExitNotification(object sender, EventArgs e)
        {
            var notification = sender as Notification;
            var newLiveNotification = new LiveNotification();
            foreach (var liveNotification in liveNotifications)
            {
                if (liveNotification.Notification == notification)
                {
                    newLiveNotification = liveNotification;
                    break;
                }
            }
            Notifications.Remove(notification);
            UpdateNotification();
            newLiveNotification.Timer.Stop();
            liveNotifications.Remove(newLiveNotification);
        }

        private void CloseNotification(object sender, EventArgs e)
        {
            var timer = sender as DispatcherTimer;
            var liveNotification = new LiveNotification();
            foreach(var _liveNotification in liveNotifications)
            {
                if(_liveNotification.Timer == timer)
                {
                    liveNotification = _liveNotification;
                    break;
                } 
            }

            Notifications.Remove(liveNotification.Notification);
            liveNotifications.Remove(liveNotification);
            UpdateNotification();
            timer.Stop();
        }

        private void UpdateNotification()
        {
            if (WaitingNotifications.Count > 0)
            {
                for (int i = 0; i < (3 - Notifications.Count); i++)
                {
                    if (WaitingNotifications.Count == 0) break;

                    var notification = WaitingNotifications[i];
                    WaitingNotifications.RemoveAt(i);
                    NotificationService.CreateNotification(notification.NotificationType, notification.Message, notification.Delay);
                }
            }
        }
        #endregion

    }
}
