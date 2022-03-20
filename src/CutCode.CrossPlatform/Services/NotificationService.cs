using System;
using CutCode.CrossPlatform.Models;

namespace CutCode.CrossPlatform.Services
{
    /// <summary>
    /// A service class for managing notifications.
    /// </summary>
    public class NotificationService
    {
        /// <summary>
        /// Defines a static class for using the same NotificationService object everywhere.
        /// </summary>
        public static NotificationService Current { get; } = new();
        
        /// <summary>
        /// An event that fires after notification is created.
        /// </summary>
        public event EventHandler? ShowNotification;
        
        /// <summary>
        /// An event that fires after notification time expires.
        /// </summary>
        public event EventHandler? OnCloseNotification;
        
        /// <summary>
        /// A method for creating a notification.
        /// </summary>
        /// <param name="notificationType">
        /// The notification type.
        /// </param>
        /// <param name="message">
        /// The notification message.
        /// </param>
        /// <param name="delay">
        /// The duration for the notification display.
        /// </param>
        /// <returns>
        /// Type of Notification
        /// </returns>
        public Notification CreateNotification(string? notificationType, string? message, int delay)
        {
            var notification = new Notification(){NotificationType = notificationType,  Message = message, Delay= delay};
            ShowNotification?.Invoke(notification, EventArgs.Empty);
            return notification;
        }

        /// <summary>
        /// A method for closing a notification.
        /// </summary>
        /// <param name="notification">
        /// The notification that will be closed.
        /// </param>
        public void CloseNotification(Notification notification)
        {
            OnCloseNotification?.Invoke(notification, EventArgs.Empty);
        }
    }
}