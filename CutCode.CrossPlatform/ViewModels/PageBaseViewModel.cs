using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using CutCode.CrossPlatform.Interfaces;
using CutCode.DataBase;
using ReactiveUI;

namespace CutCode.CrossPlatform.ViewModels
{
    public class PageBaseViewModel : ViewModelBase
    {
        public PageBaseViewModel()
        {
            ThemeService = ThemeService.Current;
            DataBase = DataBaseManager.Current;
            AssetLoader = AvaloniaLocator.CurrentMutable.GetService<IAssetLoader>();

            ThemeService.ThemeChanged += (s, e) =>
            {
                OnThemeChanged();
            };
            ThemeService.IsLightTheme = DataBase.isLightTheme;
            OnLoad();
        }

        protected virtual void OnLoad()
        {
            
        }
        
        protected virtual void OnThemeChanged()
        {
            if (ThemeService.IsLightTheme)
            {
                OnLightThemeIsSet();
            }
            else
            {
                OnDarkThemeIsSet();
            }
        }

        protected virtual void OnLightThemeIsSet()
        {
            
        }

        protected virtual void OnDarkThemeIsSet()
        {
            
        }

        protected IAssetLoader AssetLoader
        {
            get;
            set;
        }
        
        public ThemeService ThemeService
        {
            get;
            set;
        }
        
        protected DataBaseManager DataBase
        {
            get;
            set;
        }
    }
}