using Avalonia.Threading;

namespace CutCode.CrossPlatform.Models
{
    public class NotifyObject
    {
        public string NotificationType { get; set; }
        public string Message { get; set; }
        public int Delay { get; set; }
        public System.Object View { get; set; }
    }

    public class LiveNotification
    {
        public DispatcherTimer Timer { get; set; }
        public NotifyObject Notification { get; set; }
    }
}