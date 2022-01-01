using System;
using CutCode.CrossPlatform.Models;

namespace CutCode.CrossPlatform.Interfaces
{
    public interface INotificationManager
    {
        event EventHandler ShowNotification;
        NotifyObject CreateNotification(string notificationType, string message, int delay);
        void CloseNotification(NotifyObject notification);
        event EventHandler OnCloseNotification;
    }

    public class NotificationManager : INotificationManager
    {
        private static readonly NotificationManager _notificationManager = new();
        public static NotificationManager Current => _notificationManager;
        
        public event EventHandler ShowNotification;
        public NotifyObject CreateNotification(string notificationType, string message, int delay)
        {
            var notify = new NotifyObject(){NotificationType = notificationType,  Message = message, Delay= delay};
            ShowNotification?.Invoke(notify, EventArgs.Empty);
            return notify;
        }

        public event EventHandler OnCloseNotification;
        public void CloseNotification(NotifyObject notification) => OnCloseNotification?.Invoke(notification, EventArgs.Empty);
    }
}