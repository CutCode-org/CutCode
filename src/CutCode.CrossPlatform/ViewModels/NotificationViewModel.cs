using Avalonia.Media;
using CutCode.CrossPlatform.Models;
using ReactiveUI.Fody.Helpers;

namespace CutCode.CrossPlatform.ViewModels;

public class NotificationViewModel : PageBaseViewModel
{
    private readonly Notification Notification;

    public NotificationViewModel(Notification notification)
    {
        Notification = notification;
        NotificationType = Notification.NotificationType;
        Message = Notification.Message;
    }

    [Reactive] public Color Background { get; set; }

    [Reactive] public Color TextColor { get; set; }

    [Reactive] public string NotificationType { get; set; }

    [Reactive] public string Message { get; set; }

    protected override void OnLightThemeIsSet()
    {
        Background = Color.Parse("#F2F3F5");
        TextColor = Color.Parse("#0B0B13");
    }

    protected override void OnDarkThemeIsSet()
    {
        Background = Color.Parse("#2F3136");
        TextColor = Color.Parse("#CED0D4");
    }

    public void CloseNotification()
    {
        NotificationService.CloseNotification(Notification);
    }
}