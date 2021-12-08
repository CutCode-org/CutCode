using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.ReactiveUI;
using CutCode.CrossPlatform.Interfaces;
using CutCode.CrossPlatform.ViewModels;
using CutCode.CrossPlatform.Views;
using CutCode.DataBase;
using ReactiveUI;
using Splat;

namespace CutCode.CrossPlatform
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel()
                };

                desktop.Exit += (s, e) =>
                {
                    DataBaseManager.Current.ChangeTheme(ThemeService.Current.IsLightTheme);
                };
            }
            base.OnFrameworkInitializationCompleted();
        }
    }
}