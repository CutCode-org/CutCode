using System;
using Avalonia;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Themes.Fluent;
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
            PageService = PageService.Current;
            AssetLoader = AvaloniaLocator.CurrentMutable.GetService<IAssetLoader>();
            NotificationManager = NotificationManager.Current;

            ThemeService.ThemeChanged += (s, e) =>
            {
                OnThemeChanged();
            };
            OnLoad();
            OnThemeChanged();
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

        protected PageService PageService
        {
            get;
            set;
        }

        protected NotificationManager NotificationManager
        {
            get;
            set;
        }
    }
}