using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutCode.CrossPlatform.Interfaces
{
    public interface IThemeService
    {
        event EventHandler ThemeChanged;
        bool IsLightTheme { get; set; }
    }

    public class ThemeService : IThemeService
    {
        private static readonly ThemeService _themeService = new();
        public static ThemeService Current => _themeService;
        
        private ThemeService()
        {
            
        }
        
        private bool _isLightTheme;
        public bool IsLightTheme
        {
            get => _isLightTheme;
            set
            {
                _isLightTheme = value;
                ThemeChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler ThemeChanged;

    }
}
