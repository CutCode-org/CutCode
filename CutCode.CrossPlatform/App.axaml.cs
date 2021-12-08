using System;
using System.Diagnostics;
using System.Drawing;
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
                ThemeService.Current.ThemeChanged += (sender, args) =>
                {
                    if (ThemeService.Current.IsLightTheme == true)
                    {
                        Application.Current.Resources["ScrollBarTrackFill"] = new SolidColorBrush(Color.Parse("#BCBCBC"), 0.8);
                        Application.Current.Resources["ScrollBarTrackFillPointerOver"] = new SolidColorBrush(Color.Parse("#BCBCBC"), 0.8);
                        
                        Application.Current.Resources["ScrollBarButtonArrowForeground"] = new SolidColorBrush(Colors.Black, 1);
                        Application.Current.Resources["ScrollBarButtonArrowForegroundPointerOver"] = new SolidColorBrush(Colors.Black, 1);
                        Application.Current.Resources["ScrollBarButtonArrowForegroundPressed"] = new SolidColorBrush(Colors.Black, 1);
                        
                        
                        Application.Current.Resources["ScrollBarThumbFillPointerOver"] = new SolidColorBrush(Color.Parse("#B3B3B3"), 0.9);
                        Application.Current.Resources["ScrollBarThumbFillPressed"] = new SolidColorBrush(Color.Parse("#ACACAC"), 0.9);
                    }
                    else
                    {
                        Application.Current.Resources["ScrollBarTrackFill"] = new SolidColorBrush(Color.Parse("#2A2E33"), 0.8);
                        Application.Current.Resources["ScrollBarTrackFillPointerOver"] = new SolidColorBrush(Color.Parse("#24272B"), 0.8);
                        
                        Application.Current.Resources["ScrollBarButtonArrowForeground"] = new SolidColorBrush(Colors.White, 1);
                        Application.Current.Resources["ScrollBarButtonArrowForegroundPointerOver"] = new SolidColorBrush(Colors.White, 1);
                        Application.Current.Resources["ScrollBarButtonArrowForegroundPressed"] = new SolidColorBrush(Colors.White, 1);
                        
                        Application.Current.Resources["ScrollBarThumbFillPointerOver"] = new SolidColorBrush(Color.Parse("#393E44"), 0.9);
                        Application.Current.Resources["ScrollBarThumbFillPressed"] = new SolidColorBrush(Color.Parse("#3E4249"), 0.9);
                    }
                };
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