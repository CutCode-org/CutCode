using System;

namespace CutCode.CrossPlatform.Services
{
    /// <summary>
    /// A service class for the page transition.
    /// </summary>
    public class PageService
    {
        /// <summary>
        /// Defines a static class for using the same PageService object everywhere.
        /// </summary>
        public static PageService Current { get; } = new();
        
        /// <summary>
        /// An event that fires when tab or page change request is sent.
        /// </summary>
        public event EventHandler? TabChanged;
        
        /// <summary>
        /// An event that fires when external page request is sent.
        /// </summary>
        public event EventHandler? ExternalPageChange;

        private int _currentTabIndex;
        
        /// <summary>
        /// A property that provides with the Current Tab index.
        /// </summary>
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
        
        private object? _externalPage;

        /// <summary>
        /// An external page view object.
        /// </summary>
        public object? ExternalPage
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