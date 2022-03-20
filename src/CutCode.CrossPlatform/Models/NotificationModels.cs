using Avalonia.Threading;
using CutCode.CrossPlatform.Views;

namespace CutCode.CrossPlatform.Models
{
    /// <summary>
    /// A model for the notification.
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// The type of notification.
        /// </summary>
        public string? NotificationType { get; set; }
        
        /// <summary>
        /// The notification message.
        /// </summary>
        public string? Message { get; set; }
        
        /// <summary>
        /// The delay duration for the notification.
        /// </summary>
        public int Delay { get; set; }
        
        /// <summary>
        /// The view for the notification.
        /// </summary>
        public NotificationView? View { get; set; }
    }

    /// <summary>
    /// The notification that is currently in display
    /// </summary>
    public class LiveNotification
    {
        /// <summary>
        /// A type of DispatcherTimer that defines the duration of the notification
        /// </summary>
        public DispatcherTimer? Timer { get; set; }
        
        /// <summary>
        /// The notification object that is currently in display.
        /// </summary>
        public Notification? Notification { get; set; }
    }
}