using System;

namespace CutCode.CrossPlatform.Services
{
    /// <summary>
    /// An enum for the theme type.
    /// </summary>
    public enum ThemeType
    {
        /// <summary>
        /// For the light theme.
        /// </summary>
        Light = 0,
        
        /// <summary>
        /// For the dark theme.
        /// </summary>
        Dark = 1
    }
    
    /// <summary>
    /// A class for managing themes.
    /// </summary>
    public class ThemeService
    {
        /// <summary>
        /// Defines a static class for using the same ThemeService object everywhere.
        /// </summary>
        public static ThemeService Current { get; } = new();

        /// <summary>
        /// An event that fires when theme change is requested.
        /// </summary>
        public event EventHandler? ThemeChanged;
        
        private ThemeType _theme;
        
        /// <summary>
        /// A property for the current theme Object
        /// </summary>
        public ThemeType Theme
        {
            get => _theme;
            set
            {
                if (_theme != null && _theme == value) return;
                
                _theme = value;
                ThemeChanged?.Invoke(this, EventArgs.Empty);
            }
        }

    }
}