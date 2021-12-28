using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutCode.CrossPlatform.Interfaces
{
    public interface IPageService
    {
        event EventHandler TabChanged;
        int CurrentTabIndex { get; set; }

        event EventHandler ExternalPageChange;
        object ExternalPage { get; set; }

    }

    public class PageService : IPageService
    {
        private static readonly PageService _pageService = new();
        public static PageService Current => _pageService;
        public event EventHandler TabChanged;

        private int _currentTabIndex;

        public int CurrentTabIndex
        {
            get => _currentTabIndex;
            set
            {
                if (false && value == _currentTabIndex) return;
                _currentTabIndex = value;
                TabChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        
        public event EventHandler ExternalPageChange;

        private object _externalPage;
        public object ExternalPage
        {
            get => _externalPage;
            set
            {
                if (false && value == _externalPage) return;
                _externalPage = value;
                ExternalPageChange?.Invoke(this, EventArgs.Empty);
            }
        }
    }

}
