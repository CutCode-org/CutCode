using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutCode
{
    public interface INotificationManager
    {
        event EventHandler ShowNotification;
        void CreateNotification(string message, int delay);
    }

    public class NotificationManager : INotificationManager
    {
        public event EventHandler ShowNotification;
        public void CreateNotification(string message, int delay)
        {
            var notify = new NotifyModel(){ Message = message, Delay= delay};
            ShowNotification?.Invoke(notify, EventArgs.Empty);
        }
    }

    public class NotifyModel : PropertyChangedBase
    {
        public string Message { get; set; }
        public int Delay { get; set; }
        public Object View { get; set; }
    }
}
