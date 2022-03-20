using System;
using Avalonia;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Themes.Fluent;
using CutCode.CrossPlatform.Services;
using ReactiveUI;
using PageService = CutCode.CrossPlatform.Services.PageService;
using ThemeService = CutCode.CrossPlatform.Services.ThemeService;

namespace CutCode.CrossPlatform.ViewModels
{
    public class PageBaseViewModel : ViewModelBase
    {
        public PageBaseViewModel()
        {
            ThemeService = ThemeService.Current;
            DataBase = DatabaseService.Current;
            PageService = PageService.Current;
            AssetLoader = AvaloniaLocator.CurrentMutable.GetService<IAssetLoader>();
            NotificationService = NotificationService.Current;

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
            if (ThemeService.Theme == ThemeType.Light)
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

        protected IAssetLoader? AssetLoader
        {
            get;
            set;
        }
        
        public ThemeService ThemeService
        {
            get;
            set;
        }
        
        protected DatabaseService DataBase
        {
            get;
            set;
        }

        protected PageService PageService
        {
            get;
            set;
        }

        protected NotificationService NotificationService
        {
            get;
            set;
        }
    }
}