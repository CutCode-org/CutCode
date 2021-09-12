using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace CutCode
{
    public class NotifyObject
    {
        public string Message { get; set; }
        public int Delay { get; set; }
        public System.Object View { get; set; }
    }

    public class LiveNotification
    {
        public DispatcherTimer timer { get; set; }
        public NotifyObject notification { get; set; }
    }
}
