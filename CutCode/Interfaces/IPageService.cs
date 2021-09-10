using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutCode
{
    public interface IPageService
    {
        event EventHandler PageChanged;
        System.Object Page { get; set; }
        event EventHandler PageRemoteChanged;
        string remoteChange { get; set; }
        
    }

    public class PageService : IPageService
    {
        public event EventHandler PageChanged;
        private System.Object _Page { get; set; }
        public System.Object Page
        {
            get => _Page;
            set
            {
                if(_Page is null || value.GetType() != _Page.GetType())
                {
                    _Page = value;
                }
                PageChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler PageRemoteChanged;
        private string _remoteChange;
        public string remoteChange
        {
            get => _remoteChange;
            set 
            {
                _remoteChange = value;
                PageRemoteChanged?.Invoke(_remoteChange, EventArgs.Empty);
            } 
        }
    }

}
