using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using Avalonia.Media;
using Avalonia.Threading;
using CutCode.CrossPlatform.Helpers;
using CutCode.CrossPlatform.Models;
using CutCode.CrossPlatform.Services;
using CutCode.CrossPlatform.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Notification = CutCode.CrossPlatform.Models.Notification;

namespace CutCode.CrossPlatform.ViewModels;

public class MainWindowViewModel : PageBaseViewModel, IScreen
{
    public MainWindowViewModel()
    {
        Router = new RoutingState();
        Router.Navigate.Execute(new HomeViewModel(this));

        GoHome = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new HomeViewModel(this)));
        GoAdd = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new AddViewModel(this)));
        GoFavourites =
            ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new FavoritesViewModel(this)));
        GoSettings = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new SettingsViewModel(this)));

        Router.CurrentViewModel.Subscribe(x =>
            OnNaviagted(x?.GetType().Name)
        );

        GlobalEvents.OnShowCodeModel += GlobalEventsOnOnShowCodeModel;
        GlobalEvents.OnCancelClicked += GlobalEventsOnOnCancelClicked;
    }

    private void GlobalEventsOnOnCancelClicked(object? sender, EventArgs e)
    {
        Router.Navigate.Execute(new HomeViewModel(this));
    }

    private void GlobalEventsOnOnShowCodeModel(object? sender, CodeModel e)
    {
        Router.Navigate.Execute(new CodeViewModel(e, this));
    }

    [Reactive] public bool IsDarkTheme { get; set; }

    public RoutingState Router { get; }

    public event EventHandler<string?> Navigated;

    private void OnNaviagted(string? vm)
    {
        Navigated?.Invoke(this, vm);
    }

    protected override void OnLoad()
    {
        IsDarkTheme = ThemeService.Current.Theme == ThemeType.Dark;

        this.WhenAnyValue(x => x.IsDarkTheme)
            .Subscribe(x => { ThemeService.Current.Theme = IsDarkTheme ? ThemeType.Dark : ThemeType.Light; });


        NotificationService.ShowNotification += ShowNotification!;
        NotificationService.OnCloseNotification += ExitNotification!;
        Notifications = new ObservableCollection<Notification>();
    }

    protected override void OnLightThemeIsSet()
    {
        WindowsBtnColor = Color.Parse("#090909");
        BackgroundColor = Color.Parse("#FCFCFC");
        TitleBarColor = Color.Parse("#E3E5E8");
        SideBarColor = Color.Parse("#F2F3F5");
        MainTextColor = Color.Parse("#0B0B13");

        TitlebarBtnsHoverColor = Color.Parse("#D0D1D2");
        MenuButtonColour = Color.Parse("#0B0B13");
    }

    protected override void OnDarkThemeIsSet()
    {
        WindowsBtnColor = Color.Parse("#F2F2F2");
        BackgroundColor = Color.Parse("#36393F");
        TitleBarColor = Color.Parse("#202225");
        SideBarColor = Color.Parse("#32363C");
        MainTextColor = Color.Parse("#94969A");

        TitlebarBtnsHoverColor = Color.Parse("#373737");
        MenuButtonColour = Color.Parse("#94969A");
    }

    #region Color

    [Reactive] public Color WindowsBtnColor { get; set; }

    [Reactive] public Color BackgroundColor { get; set; }

    [Reactive] public Color TitleBarColor { get; set; }

    [Reactive] public Color SideBarColor { get; set; }

    [Reactive] public Color MainTextColor { get; set; }

    [Reactive] public Color TitlebarBtnsHoverColor { get; set; }

    [Reactive] public Color MenuButtonColour { get; set; }

    #endregion

    #region Commands

    public ReactiveCommand<Unit, IRoutableViewModel> GoHome { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoAdd { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoFavourites { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoSettings { get; }

    #endregion

    #region NotificationDialogView

    [Reactive] public ObservableCollection<Notification> Notifications { get; set; }

    private readonly List<Notification> WaitingNotifications = new();
    private readonly List<LiveNotification> liveNotifications = new();

    private void ShowNotification(object sender, EventArgs e)
    {
        Notification? notification = sender as Notification;

        NotificationView notifcationView = new()
        {
            DataContext = new NotificationViewModel(notification!)
        };
        notification.View = notifcationView;

        if (Notifications.Count > 2)
        {
            WaitingNotifications.Add(notification);
        }
        else
        {
            Notifications.Add(notification);

            DispatcherTimer closeTimer = new()
            {
                Interval = TimeSpan.FromSeconds(notification.Delay),
                IsEnabled = true
            };
            liveNotifications.Add(new LiveNotification { Timer = closeTimer, Notification = notification });
            closeTimer.Tick += CloseNotification!;
        }
    }

    private void ExitNotification(object sender, EventArgs e)
    {
        Notification? notification = sender as Notification;
        LiveNotification newLiveNotification = new();
        foreach (LiveNotification liveNotification in liveNotifications)
            if (liveNotification.Notification == notification)
            {
                newLiveNotification = liveNotification;
                break;
            }

        Notifications.Remove(notification);
        UpdateNotification();
        newLiveNotification.Timer.Stop();
        liveNotifications.Remove(newLiveNotification);
    }

    private void CloseNotification(object sender, EventArgs e)
    {
        DispatcherTimer? timer = sender as DispatcherTimer;
        LiveNotification liveNotification = new();
        foreach (LiveNotification _liveNotification in liveNotifications)
            if (_liveNotification.Timer == timer)
            {
                liveNotification = _liveNotification;
                break;
            }

        Notifications.Remove(liveNotification.Notification);
        liveNotifications.Remove(liveNotification);
        UpdateNotification();
        timer.Stop();
    }

    private void UpdateNotification()
    {
        if (WaitingNotifications.Count > 0)
            for (int i = 0; i < 3 - Notifications.Count; i++)
            {
                if (WaitingNotifications.Count == 0) break;

                Notification notification = WaitingNotifications[i];
                WaitingNotifications.RemoveAt(i);
                NotificationService.CreateNotification(notification.NotificationType, notification.Message,
                    notification.Delay);
            }
    }

    #endregion
}