using Avalonia.Media;
using CutCode.CrossPlatform.Interfaces;
using CutCode.CrossPlatform.Models;
using ReactiveUI;

namespace CutCode.CrossPlatform.ViewModels
{
    public class NotificationViewModel : PageBaseViewModel
    {
        private Notification Notification;
        public NotificationViewModel(Notification notification)
        {
            Notification = notification;
            NotificationType = Notification.NotificationType;
            Message = Notification.Message;
        }
        
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
        
        private Color _background;
        public Color Background
        {
            get => _background;
            set => this.RaiseAndSetIfChanged(ref _background, value);
        }
        
        private Color _textColor;
        public Color TextColor
        {
            get => _textColor;
            set => this.RaiseAndSetIfChanged(ref _textColor, value);
        }

        private string _notificationType;
        public string NotificationType
        {
            get => _notificationType;
            set => this.RaiseAndSetIfChanged(ref _notificationType, value);
        }
        
        private string _message;
        public string Message
        {
            get => _message;
            set => this.RaiseAndSetIfChanged(ref _message, value);
        }

        public void CloseNotification() => NotificationService.CloseNotification(Notification);
    }
}