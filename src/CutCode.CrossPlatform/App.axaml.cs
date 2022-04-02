using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.ReactiveUI;
using CutCode.CrossPlatform.Helpers;
using CutCode.CrossPlatform.ViewModels;
using CutCode.CrossPlatform.Views;
using CutCode.CrossPlatform.Services;
using ReactiveUI;
using Splat;
using Color = Avalonia.Media.Color;

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
                if(ThemeService.Current.Theme == ThemeType.Light) SystemColorsConfig.LightThemeColors();
                else SystemColorsConfig.DarkThemeColors();
                ThemeService.Current.ThemeChanged += (sender, args) =>
                {
                    if(ThemeService.Current.Theme == ThemeType.Light) SystemColorsConfig.LightThemeColors();
                    else SystemColorsConfig.DarkThemeColors();
                };
                
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel()
                };
                
                ThemeService.Current.Theme = DatabaseService.Current.Theme;

                desktop.Exit += (s, e) =>
                {
                    DatabaseService.Current.ChangeTheme(ThemeService.Current.Theme);
                };
                var updateThread = new Thread(UpdateChecker.Run);
                updateThread.Start();
            }
            base.OnFrameworkInitializationCompleted();
        }
    }
}